/// <reference path="..\breeze.debug.js" />
/// <reference path="../knockout-2.3.0.debug.js" />

(function (root) {
    var ko = root.ko,
        breeze = root.breeze;

    var serviceName = 'breeze/Data';

    var metadataStore = new breeze.MetadataStore();
    var manager = new breeze.EntityManager({
        serviceName: serviceName,
        metadataStore: metadataStore
    });

    var user = {
        PIN: ko.observable('01.137.077/0007-27'),
        CompanyName: ko.observable().extend({ required: true }),
        Password: ko.observable().extend({ required: true }),

        ReceivePromotions: ko.observable(),
        CallSpecialist: ko.observable(),

        Adress: {

            City: ko.observable().extend({ required: true }),
            Number: ko.observable().extend({ required: true }),
            State: ko.observable().extend({ required: true }),
            StreetName: ko.observable().extend({ required: true }),
            Suite: ko.observable().extend({ required: true }),
            Zipcode: ko.observable().extend({ required: true })
        },

        Contact: {

            MainContact: ko.observable().extend({ required: true }),
            Phone: ko.observable().extend({ required: true }),
            Position: ko.observable().extend({ required: true })
        },

        Email: ko.observable().extend({
            required: true,
            email: true
        }),

        confirmEmail: ko.observable().extend({ required: true })
    },

    currentStep = ko.observable(1),

    confirmEmail = ko.observable(),

    cmdConsultPIN = ko.asyncCommand({
        execute: function (complete) {
            getDocumentByPIN(user.PIN());
            complete();
        },

        canExecute: function (isExecuting) {
            return !isExecuting && user ? user.PIN() : false;
        }
    }),

searchEmail = VerifyExintingEmail;


    function getDocumentByPIN() {

        var query = breeze.EntityQuery.from("Document").withParameters({ pin: user.PIN() });

        return manager
            .executeQuery(query)
            .then(querySucceeded)
            .fail(queryFailed);

        function querySucceeded(data) {
            currentStep(2);
        }

        function queryFailed(data) {
            currentStep(1);
            $('.form-error-message[data-validation=ein]').show();
        }
    };

    function VerifyExintingEmail() {
        var query = breeze.EntityQuery.from("VerifyExintingEmail").withParameters({ email: user().Email() });

        return manager
            .executeQuery(query)
            .then(querySucceeded);

        function querySucceeded(data) {
            existingMail(!data.results[0]);
            user.isValid();
        }
    }

    function registerCustomRules() {
        ko.validation.init({
            parseInputAttributes: true,
            writeInputAttributes: true,
            decorateElement: true
        });

        ko.validation.rules['verifyMail'] = {
            async: true,
            validator: function (val, otherVal, callback) {
                var query = breeze.EntityQuery.from("VerifyExintingEmail").withParameters({ email: user.Email() });

                return manager
                    .executeQuery(query)
                    .then(querySucceeded);

                function querySucceeded(data) {
                    callback({ isValid: !data.results[0], message: 'The mail already exists.' });
                }
            },
            message: 'The mail already exists.'
        };

        ko.validation.rules['mustEqual'] = {
            validator: function (val, otherVal) {
                return user.Email() === user.confirmEmail();
            },
            message: 'The mail must be equal.'
        };

        ko.validation.registerExtenders();
    }

    var vm = {
        currentStep: currentStep,
        user: user,
        cmdConsultPIN: cmdConsultPIN,
        searchEmail: searchEmail
    };


    (function init() {
        manager.fetchMetadata().done(function () {

            registerCustomRules();

            //user(manager.createEntity("User"));

            user.Email.extend({
                verifyMail: true
            });

            user.confirmEmail.extend({ mustEqual: true });

            ko.applyBindings(vm);

            $('.form-submit').click(function (e) {
                e.preventDefault();

                if (ko.validation.group(vm.user, { deep: true })().length === 0) {
                    $('form').submit();
                } else {
                    alert('Form invalid!');
                }
            });
        });

    })();

}(window));