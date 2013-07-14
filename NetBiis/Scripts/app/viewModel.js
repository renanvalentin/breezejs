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

    var user = ko.observable(),

        currentStep = ko.observable(1),

        confirmEmail = ko.observable(),

        cmdConsultPIN = ko.asyncCommand({
            execute: function (complete) {
                getDocumentByPIN(user().PIN());
                complete();
            },

            canExecute: function (isExecuting) {
                return !isExecuting && user() ? user().PIN() : false;
            }
        }),

        searchEmail = VerifyExintingEmail;


    function getDocumentByPIN() {

        var query = breeze.EntityQuery.from("Document").withParameters({ pin: user().PIN() });

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

        ko.validation.rules['exampleAsync'] = {
            async: true, 
            validator: function (val, otherVal, callback) { 
                var query = breeze.EntityQuery.from("VerifyExintingEmail").withParameters({ email: user().Email() });

                return manager
                    .executeQuery(query)
                    .then(querySucceeded);

                function querySucceeded(data) {
                    callback({ isValid: !data.results[0], message: 'The mail already exists.' });
                }
            },
            message: 'The mail already exists.'
        };

        ko.validation.registerExtenders();
    }

    var vm = {
        currentStep: currentStep,
        user: user,
        confirmEmail: confirmEmail,
        cmdConsultPIN: cmdConsultPIN,
        searchEmail: searchEmail
    };


    (function init() {
        manager.fetchMetadata().done(function () {
            registerCustomRules();

            user(manager.createEntity("User"));

            user().PIN('01.137.077/0007-27');

            user().CompanyName.extend({ required: true });

            user().Password.extend({ required: true });

            user().Adress().City.extend({ required: true });
            user().Adress().Number.extend({ required: true });
            user().Adress().State.extend({ required: true });
            user().Adress().StreetName.extend({ required: true });
            user().Adress().Suite.extend({ required: true });
            user().Adress().Zipcode.extend({ required: true });

            user().Contact().MainContact.extend({ required: true });
            user().Contact().Phone.extend({ required: true });
            user().Contact().Position.extend({ required: true });

            user().Email.extend({
                required: true,
                email: true,
                exampleAsync: true
            });

            confirmEmail.extend({
                validation: {
                    validator: function (val, someOtherVal) {
                        return user().Email() === confirmEmail();
                    },
                    message: 'The mail must be equal.',
                    params: 1,
                    rule: 'equalTo'
                }
            });

            ko.applyBindings(vm);

            $('.form-submit').click(function (e) {
                e.preventDefault();

                if (ko.validation.group(user())().length === 0) {
                    $('form').submit();
                } else {
                    alert('Form invalid!');
                }
            });
        });

    })();

}(window));