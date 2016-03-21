angular.module('traktat.ui')
.directive('trkPhoneList', function(GlobalsService) {
    return {
        scope: {
            items: '='
        },
        templateUrl: 'app/providers/contacts/telephones/trkPhoneList.tpl.html',
        
        controller: function($scope) {

            $scope.lists = {
                phoneTypes : GlobalsService.get('phoneTypes')
            };

            $scope.remove = function(item) {
                var index = $scope.items.indexOf(item);
                $scope.items.splice(index, 1);
            };

            $scope.addTelNo = function() {
                $scope.items.push(createItem());

                function createItem() {
                    return {};
                }
            };
        }
    };
})