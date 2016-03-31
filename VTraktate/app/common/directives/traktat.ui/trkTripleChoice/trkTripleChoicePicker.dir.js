(() => {
    
    angular.module("traktat.ui")
        .directive("trkTripleChoicePicker", function() {
            return {
                templateUrl: "app/common/directives/traktat.ui/trkTripleChoice/trkTripleChoicePicker.tpl.html",
                restrict: "E",
                scope: {
                    selection: "="
                },
                controller: function($scope) {
                    $scope.items = [{ title: "-" }, { id: true, title: "Да" }, { id: false, title: "Нет" }];
                }
            };
        });

})();