(() => {
    angular.module("app")
        .factory("mainNavMenu", ($rootScope) => {
            return [
                {
                    text: "Админ",
                    glyph: "glyphicon glyphicon-cog",
                    state: "admin",
                    subitems:
                    [
                        { text: "Аккаунты", state: "admin.account" }
                    ]
                },
                {
                    text: "Кадры",
                    glyph: "glyphicon glyphicon-user",
                    state: "hr",
                    subitems:
                    [
                        { text: "Сотрудники", state: "hr.employees" },
                        { text: "Оценки", state: "grades.list" },
                        { text: "Табель", state: "hr.workTimeSheet" }
                    ]

                },
                {
                    text: "Заказы",
                    glyph: "glyphicon glyphicon-tasks",
                    state: "orders",
                    subitems:
                    [
                        { text: "Список", state: "orders.list" }
                    ]

                },
                { text: "Выйти", glyph: "glyphicon glyphicon-log-out", action: logout }
            ];

            function logout() {
                $rootScope.$emit("logout");
            }
        });

})();