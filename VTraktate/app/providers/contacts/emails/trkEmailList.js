angular.module('traktat.ui')
.directive('trkEmailList', function() {
    return {
        scope: {
            items: '='
        },
        templateUrl: 'app/providers/contacts/emails/trkEmailList.tpl.html',
        //controllerAs: 'model',
        controller: function($scope) {

            $scope.remove = function(item) {
                var index = $scope.items.indexOf(item);
                $scope.items.splice(index, 1);
            };

            $scope.addContactPerson = function() {
                $scope.items.push(createItem());

                function createItem() {
                    return {};
                }
            };
        }
    };
})