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

        existingMail = false,

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

        cmdSave = ko.asyncCommand({
            execute: function (complete) {
                saveChanges();
                complete();
            },

            canExecute: function (isExecuting) {
                return !isExecuting;
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
            if (data.results[0]) {

            }
        }
    }

    function saveChanges() {
        if (!user().entityAspect.validateEntity()) {
            console.log('adsf');
        } else {
            console.log('f');
        }

        //return manager.saveChanges()
        //    .then(function () { logger.success("changes saved"); })
        //    .fail(saveFailed);
    }

    function registerCustomRules() {
        ko.validation.init({
            parseInputAttributes: true,
            writeInputAttributes: true,
            decorateElement: true
        });
    }

    var vm = {
        currentStep: currentStep,
        user: user,
        confirmEmail: confirmEmail,
        cmdConsultPIN: cmdConsultPIN,
        cmdSave: cmdSave,
        searchEmail: searchEmail
    };


    (function init() {
        manager.fetchMetadata().done(function () {
            registerCustomRules();

            user(manager.createEntity("User"));

            user().PIN('01.137.077/0007-27');

            // user().Document.extend({ required: true });
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

            user().Email.extend({ required: true, email: true, validation: {
                validator: function (val, someOtherVal) {
                    return user().Email() === confirmEmail();
                },
                message: 'The mail already exists.',
                params: 5
            });

            confirmEmail.extend({
                validation: {
                    validator: function (val, someOtherVal) {
                        return user().Email() === confirmEmail();
                    },
                    message: 'The mail must be equal.',
                    params: 5
                }
            });

            ko.applyBindings(vm);
        });

    })();

}(window));