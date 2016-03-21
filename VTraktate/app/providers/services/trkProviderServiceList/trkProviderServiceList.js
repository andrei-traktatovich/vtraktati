angular.module('app')
.directive('trkProviderServiceList', function (GlobalsService) {
    return {
        scope: {
            items: '='
        },
        templateUrl: 'app/providers/services/trkProviderServiceList/trkProviderServiceList.tpl.html',
        controller: function($scope) {
            $scope.isAddingItems = false;

            function setServiceTypeList() {
                $scope.serviceTypes = GlobalsService.getExcept('serviceTypes', $scope.items, 'type.id');
            }

            setServiceTypeList();

            clearAddenda();

            $scope.$watchCollection('items', setServiceTypeList);

            function clearAddenda() {
                $scope.newItems = {
                    services: []
                };
            }

            $scope.remove = function (item) {
                var index = $scope.items.indexOf(item);
                $scope.items.splice(index, 1);
            };
            

            function createService(item) {

                return {
                    type: { 
                        id: item.id,
                        name: item.title
                    },
                    stars: 2,
                    currencyId: 1,
                    uomId: item.requiresLanguage ? 2 : 1,
                    isLinguistic: item.requiresLanguage,
                    languages: []
                };
            }

            $scope.addItems = function() {
                $scope.newItems.services.forEach(function (item) {
                    var newItem = createService(item)
                    $scope.items.push(newItem);
                });

                $scope.isAddingItems = false;
                clearAddenda();
                
            };

            $scope.beginAddItems = function() {
                clearAddenda();
                $scope.isAddingItems = true;
            }

            $scope.cancelAddItems = function () {
                $scope.isAddingItems = false;
                clearAddenda();
            }
        }
    };
})