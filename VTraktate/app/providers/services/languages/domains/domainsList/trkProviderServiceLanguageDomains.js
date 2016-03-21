angular.module('app')
.directive('trkProviderServiceLanguageDomains', function (GlobalsService) {
    var DEFAULT_DOMAINS_STARS = 0;
    return {
        scope: {
            items: '='
        },
        templateUrl: 'app/providers/services/languages/domains/domainsList/trkProviderServiceLanguageDomains.tpl.html',
        controller: function ($scope) {
            $scope.addingItems = false;

            setDomainsList();
            var allDomains = [];

            function setDomainsList() {
                allDomains = GlobalsService.getExcept('domains', $scope.items, 'domain.id');
            }

            $scope.$watchCollection('items', setDomainsList);

            function findInDomains(itemId) {
                /* TODO : do something more sensible here */
                for(var i=0; i< allDomains.length; i++) {
                    var member = allDomains[i];
                    if (member.id === itemId)
                        return { id: member.id, name: member.title };
                }
                return null;
            }

            clearAddenda();

            function clearAddenda() {
                $scope.newItems = {
                    domains: []
                };
            }

            function createItem(item) {
                var domain = findInDomains(item);
                if (domain)
                    return {
                        domain: domain,
                        stars: DEFAULT_DOMAINS_STARS,
                        comment: ''
                    };
                else
                    return null;
            }

            $scope.remove = function (item) {
                var index = $scope.items.indexOf(item);
                $scope.items.splice(index, 1);
            }

            $scope.addItems = function () {
                $scope.newItems.domains.forEach(function (item) {
                    var newItem = createItem(item)
                    if(newItem)
                        $scope.items.push(newItem);
                });

                $scope.addingItems = false;
                clearAddenda();
            };

            $scope.beginAddItems = function () {
                clearAddenda();
                $scope.addingItems = true;
            }

            $scope.cancelAddItems = function () {
                $scope.addingItems = false;
                clearAddenda();
            }
        }
    };
})