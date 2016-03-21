angular.module('traktat.ui')
.directive('trkProfileOtherContactsList', function (GlobalsService) {
    
    return {
        scope: {
            items: '=',
            contactPersonId: '=',
            readonly: '='
            
        },
        templateUrl: 'app/providers/profile/contacts/otherContacts/trkProfileOtherContactsList.tpl.html',
        controllerAs: 'model',
        controller: function ($scope, $http, confirmer) {
            var model = this;
            $scope.lists = {
                types: GlobalsService.get('otherContactTypes')
            };
            model.items = $scope.items || [];

            restoreState();

            model.beginAddItem = function () {
                model.addingItem = true;
                model.editingItem = false;
                model.newItem = {};
            }

            model.beginEditItem = function (item) {
                model.addingItem = false;
                model.editingItem = true;

                model.newItem = {
                    id: item.id,
                    address: item.address,
                    active: item.active,
                    typeId: item.type.id,
                    comment: item.comment,
                    isDeleted: false
                };

                console.log('start editing item');
                console.log(item);
                model.selectedIndex = model.items.indexOf(item);
            }

            model.addItem = function () {
                console.log('adding new phone number to db')
                var url = 'api/people/' + $scope.contactPersonId + '/otherContact';
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
                model.newItem = {
                    id: item.id,
                    address: item.address,
                    active: item.active,
                    typeId: item.type.id,
                    comment: item.comment,
                    isDeleted: false
                };
                model.editItem();
            }

            model.editItem = function () {
                var url = 'api/otherContact/' + model.newItem.id;
                $http.put(url, model.newItem)
                .success(success)
                .error(failure);

                function success(result) {
                    model.items[model.selectedIndex] = result;
                    restoreState();
                    toastr.success('Изменение/восстановление адреса успешно')
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

                    $http.delete('api/otherContact/' + item.id)
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