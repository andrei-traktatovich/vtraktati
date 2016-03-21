angular.module('traktat.ui')
.directive('trkProfilePhonesList', function (GlobalsService) {
    return {
        scope: {
            items: '=',
            contactPersonId: '=',
            readonly: '='
            
        },
        templateUrl: 'app/providers/profile/contacts/phones/phonesList/trkProfilePhonesList.tpl.html',
        controllerAs: 'model',
        controller: function ($scope, $http, confirmer) {
            var model = this;
            $scope.lists = {
                phoneTypes: GlobalsService.get('phoneTypes')
            };
            model.items = $scope.items || [];

            // need to sanitize phone number because 1st time it passes +before phone number to the SIP app
            $scope.sanitizePhoneNumber = sanitizePhoneNumber;
            function sanitizePhoneNumber(number) {
                // remove all non-numeric characters
                return angular.isString(number) ? number.replace(/\D/g, '') : '';
            }
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
                console.log('start editing item');
                console.log(item);
                model.selectedIndex = model.items.indexOf(item);
            }

            model.addItem = function () {
                console.log('adding new phone number to db')
                var url = 'api/people/' + $scope.contactPersonId + '/phones';
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
                var url = 'api/phones/' + model.newItem.id;
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

                    $http.delete('api/phones/' + item.id)
                    .success(success)
                    .error(failure);

                    function success() {
                        if (item.hasOwnProperty("isDeleted"))
                            item.isDeleted = true;
                        else {
                            var index = model.items.indexOf(item);
                            model.items.splice(index, 1);
                        }
                        toastr.info('Номер телефона удален.')
                    }

                    function failure() {
                        toastr.error('Я ошипко')
                    }
                }
            }
        }
    };
});