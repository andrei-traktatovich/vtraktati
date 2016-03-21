(function () {
    angular.module('traktat.ui')
    .directive('trkProfileEmailsList', function () {
        return {
            restrict: 'E',
            scope: {
                items: '=',
                readonly: '=',
                contactPersonId: '='
            },
            templateUrl: 'app/providers/profile/contacts/emails/emailsList/trkProfileEmailsList.tpl.html',
            controllerAs: 'model',
            controller: function ($scope, $http, confirmer) {

                var model = this;

                model.concatEmails = concatEmails($scope.items);

                model.items = $scope.items || [];

                $scope.$watchCollection('items', function () {
                    $scope.concatEmails = concatEmails($scope.items);
                });

                restoreState();

                model.beginAddItem = function () {
                    model.addingItem = true;
                    model.editingItem = false;
                    model.newItem = {};
                }

                model.beginEditItem = function (item) {
                    model.addingItem = false;
                    model.editingItem = true;
                    model.newItem = angular.copy(item);
                    model.selectedIndex = model.items.indexOf(item);
                }

                model.addItem = function () {
                    var url = 'api/people/' + $scope.contactPersonId + '/emails';
                    $http.post(url, model.newItem)
                    .success(success)
                    .error(failure);

                    function success(result) {
                        model.items.push(result);
                        restoreState();
                    }

                    function failure() {
                        toastr.error('Я ошипко');
                    }
                }

                model.restoreItem = function (item) {
                    item.isDeleted = false;
                    model.newItem = item;
                    model.editItem();
                }

                model.editItem = function () {
                    var url = 'api/emails/' + model.newItem.id;
                    $http.put(url, model.newItem)
                    .success(success)
                    .error(failure);

                    function success(result) {
                        model.items[model.selectedIndex] = result;
                        restoreState();
                    }

                    function failure() {
                        toastr.error('Я ошипко');
                    }
                }

                function restoreState() {
                    model.addingItem = false;
                    model.editingItem = false;
                    model.newItem = {};
                }

                model.cancelItem = function () {
                    restoreState();
                }


                model.removeItem = function (item) {
                    if (confirmer.yes('Вы действительно хотите удалить адрес?')) {

                        $http.delete('api/emails/' + item.id)
                        .success(success)
                        .error(failure);

                        function success() {
                            if (item.hasOwnProperty("isDeleted"))
                                item.isDeleted = true;
                            else {
                                var index = model.items.indexOf(item);
                                model.items.splice(index, 1);
                            }
                            toastr.info('Адрес электропочты удален.')
                        }

                        function failure() {
                            toastr.error('Я ошипко')
                        }
                    }
                }

                function concatEmails(emailAddressItems) {
                    if (!emailAddressItems || emailAddressItems.length < 1)
                        return null;
                    var result = emailAddressItems.map(function (item) { return item.email; });
                    if (result.length && result.length > 0)
                        return result.join('; ');

                }
            }
        };
    });

})();