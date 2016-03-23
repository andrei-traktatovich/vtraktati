(() => {

    angular.module("app")
        .value("routes", [
                {
                    name: "login",
                    state: {
                        templateUrl: "app/auth/login/login.view.html",
                        url: "/login",
                        controller: "loginCtrl",
                        data: { auth: { noLogin: true } }
                    }
                },
                {
                    name: "home",
                    state: {
                        url: "/",
                        templateUrl: "app/home/home.view.html",
                        controller: "homeCtrl",
                        data: { auth: { noLogin: false } }
                    }
                },
                {
                    name: "admin",
                    state: {
                        abstract: true,
                        template: "<ui-view/>",
                        data: { auth: { roles: [1] } }
                    }
                },
                {
                    name: "admin.account",
                    state: {
                        url: "/admin/accounts",
                        templateUrl: "app/admin/accounts/accounts.view.html",
                        controller: "accountsCtrl",
                        data: {
                            tableParams: null, // initially, nothing 
                            tableDefaults: {
                                page: 1,
                                count: 12,
                                sorting: {},
                                filter: {},
                                total: 0
                            },
                            auth: { roles: [1] }
                        }
                    }
                },
                {
                    name: "admin.accountCreate",
                    state: {
                        templateUrl: "app/admin/accounts/accounts.edit.view.html",
                        controller: "accountCreateCtrl",
                        data: { auth: { roles: [1] } },
                        params: { personId: null, personName: null, creatingAccount: null }
                    }
                },
                {
                    name: "admin.accountEdit",
                    state: {
                        templateUrl: "app/admin/accounts/accounts.edit.view.html",
                        controller: "accountEditCtrl",
                        data: { auth: { roles: [1] } },
                        params: {
                            account: {
                                accountId: null,
                                email: null,
                                password: null,
                                confirmPassword: null,
                                roles: [],
                                loginDisabled: false,
                                name: null,
                            },
                            personName: null
                        }
                    }
                },
                {
                    name: "hr",
                    state: {
                        abstract: true,
                        template: "<ui-view></ui-view>",
                        data: { auth: { roles: [1, 11, 5, 6, 4, 8, 9, 7, 12] } }
                    }
                },
                {
                    name: "hr.employees",
                    state: {
                        url: "/hr/employees",
                        templateUrl: "app/providers/providers.view.html",
                        controller: "providersCtrl",
                        data: {
                            tableParams: null, // initially, nothing 
                            tableDefaults: {
                                page: 1,
                                count: 12,
                                sorting: {},
                                filter: null,
                                total: 0
                            },

                            auth: { roles: [1, 11, 5, 6, 7, 12] }
                        }
                    }
                },
                {
                    name: "hr.employees.profile",
                    state: {
                        templateUrl: "app/providers/profile/provider-profile.tpl.html",
                        params: {
                            id: 0
                        },
                        controller: "providerProfileController"
                    }
                },
                {
                    name: "hr.createIndividual",
                    state: {
                        url: "/hr/create",
                        params: { type: { id: 1, title: "Фрилансер" } },
                        templateUrl: "app/providers/create/individual/templates/individualCreateForm.tpl.html",
                        controller: "individualCreateFormCtrl"
                    }
                },
                {
                    name: "hr.modifyEmployment",
                    state: {
                        params: { id: null },
                        templateUrl: ""
                    }
                },
                {
                    name: "hr.workTimeSheet",
                    state: {
                        url: "/hr/worktimesheet",
                        template: "<h2>Табель</h2>",
                        data: { auth: { roles: [1, 12] } },
                    }
                }
            ]);

})();

        


