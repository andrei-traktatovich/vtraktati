angular.module('app')
.directive('trkProviderServiceLanguages', function (GlobalsService) {
    var DEFAULT_LANGUAGE_STARS = 0;
    return {
        scope: {
            items: '='
        },
        templateUrl: 'app/providers/services/languages/trkProviderServiceLanguageList/trkProviderServiceLanguageList.tpl.html',
        controller: function ($scope) {
            $scope.isAddingItems = false;

            function setLanguageList() {
                $scope.languages = GlobalsService.getExcept('languages', $scope.items, 'languagePair.id');
            }
            setLanguageList();

            $scope.$watchCollection('items', setLanguageList);

            $scope.domains = GlobalsService.get('domains');

            clearAddenda();

            function clearAddenda() {
                $scope.newItems = {
                    languages: [],
                    domains: []
                };
            }

            function createItem(item, domains) {
                var domainInfos = makeDomainModels(domains);

                return {
                    languagePair: {
                        id: item.id,
                        name: item.title
                    },
                    stars: DEFAULT_LANGUAGE_STARS,
                    domains: domainInfos
                };
            }

            function makeDomainModels(domains) {
                if (!domains || !domains.length)
                    return [];
                var result = domains.map(function (item) { return { comment: '', domain: { id: item.id, name: item.title }, stars: 0 }; });
                return result;
            }

            $scope.remove = function (item) {
                var index = $scope.items.indexOf(item);
                $scope.items.splice(index, 1);
            }

            $scope.addItems = function () {
                $scope.newItems.languages.forEach(function (item) {
                    var newItem = createItem(item, $scope.newItems.domains)
                    $scope.items.push(newItem);
                });

                $scope.isAddingItems = false;
                clearAddenda();
            };

            $scope.beginAddItems = function () {
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