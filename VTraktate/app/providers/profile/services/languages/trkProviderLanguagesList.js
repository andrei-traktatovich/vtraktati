(function () {
    angular.module('app')
        .directive('trkProviderLanguagesList', trkProviderLanguagesList)
        .service('addLanguageToProfileDialog', addLanguageToProfileDialog)
        .service('editExistingLanguageDialog', editExistingLanguageDialog)
        .service('languagesManager', languagesManager)
        .controller('trkProviderProfileLanguagesListController', trkProviderProfileLanguagesListController)
        .controller('addLanguageToProfileDialogController', addLanguageToProfileDialogController)
        .controller('editExistingLanguageDialogController', editExistingLanguageDialogController);
        

    function trkProviderLanguagesList() {
        return {
            scope: {
                items: '=',
                parentId: '=parent',
                readonly: "="
            },
            controllerAs: 'model',
            controller: 'trkProviderProfileLanguagesListController',
            templateUrl: 'app/providers/profile/services/languages/trkProviderLanguagesList.tpl.html'
        };
    }

    function trkProviderProfileLanguagesListController($scope, addLanguageToProfileDialog, editExistingLanguageDialog, languagesManager, confirmer, toastr, _) {

        $scope.addLanguages = addLanguages;
        $scope.edit = editLanguage;
        $scope.remove = removeLanguage;

        function addLanguages() {
            var config = {
                exclude: $scope.items
            },
                model = {
                    serviceId: $scope.parentId,
                    stars: 0,
                    nativeSpeaker: false,
                    languages: [],
                    minRate: null,
                    maxRate: null,
                    productivityMin: null,
                    productivityMax: null
                };

            addLanguageToProfileDialog
                .open(model, config)
                .then(updateList);

            function updateList(items) {
                $scope.items = $scope.items.concat(items);
                toastr.info('Данные успешно сохранены');
            }
        };

        function editLanguage(language) {
            var config = {
                languagePairName: language.languagePairName
            };
            var model = makeModel(language);

            editExistingLanguageDialog
                .open(model, config)
                .then(updateList);

            return deferred.promise;

            function makeModel(language) {
                // need to do it more elegantly ...
                return {
                    id: language.id,
                    serviceId: language.serviceId,
                    qaStars: language.qaStars,
                    languagePairId: language.languagePairId,
                    minRate: language.minRate,
                    maxRate: language.maxRate,
                    qaComment: language.qaComment,
                    nativeSpeaker: language.nativeSpeaker,
                    comment: language.comment,
                    productivityMin: language.productivityMin,
                    productivityMax: language.productivityMax
                };
            }

            function updateList(newData) {
                //language.qaStars = newData.qaStars;
                //language.minRate = newData.minRate;
                //language.maxRate = newData.maxRate;
                //language.qaComment = newData.qaComment;
                //language.comment = newData.comment;
                //language.modifiedByName = newData.modifiedByName;
                //language.modifiedDate = newData.modifiedDate;
                var index = $scope.items.indexOf(language);
                $scope.items[index] = newData;
                toastr.success('Данные о направлении ' + language.languagePairName + ' обновлены');
            } 
        }

        function removeLanguage(item) {
            if (confirmer.yes('Вы действительно хотите удалить язык ' + item.languagePairName + '?')) {
                languagesManager
                    .remove(item.id)
                    .success(removeLanguage)
                    .error(displayError);

                function removeLanguage() {
                    $scope.items = _.without($scope.items, item);
                    toastr.success('Направление ' + item.languagePairName + ' удалено');
                }

                function displayError() {
                    toastr.error("Я ошипко!");
                }
            }
        }

    }

    function addLanguageToProfileDialog($modal) {

        return {
            open: open
        };

        function open(model, config) {
            var modalInstance = $modal.open({
                templateUrl: 'app/providers/profile/services/languages/add/trkProviderlanguageAdd.tpl.html',
                resolve: {
                    model: function () { return model; },
                    config: function () { return config; }
                },
                controller: 'addLanguageToProfileDialogController'
            });
            return modalInstance.result;
        }
    }

    // looks like this is a violation of the DRY principle (add and edit are almost completely the same ... 
    function editExistingLanguageDialogController($scope, $modalInstance, languagesManager, model, config) {
        $scope.model = model;
        $scope.ok = ok;
        $scope.cancel = cancel;
        $scope.languagePairName = config.languagePairName;
        
        function ok() {
            languagesManager
                .update($scope.model)
                .success(closeDialog)
                .error(displayError);

            function closeDialog(data) {
                $modalInstance.close(data);
            }

            function displayError(error, status) {
                $scope.errorMessage = error; // TODO: clarify the structure of error messages
            }
        }

        function cancel() {
            $modalInstance.dismiss();
        }
    }


    function addLanguageToProfileDialogController($scope, $modalInstance, languagesManager, model, config) {

        $scope.existingLanguages = config.exclude;
        $scope.model = model;
        $scope.ok = ok;
        $scope.cancel = cancel;

        function ok() {
            languagesManager
                .add($scope.model)
                .success(closeDialog)
                .error(displayError);

            function closeDialog(data) {
                $modalInstance.close(data);
            }

            function displayError(error, status) {
                $scope.errorMessage = error; // TODO: clarify the structure of error messages
            }
        }
        
        function cancel() {
            $modalInstance.dismiss();
        }
    }

    
    function editExistingLanguageDialog($modal) {
        return {
            open: open
        };

        function open(model, config) {
            var modalInstance = $modal.open({
                templateUrl: 'app/providers/profile/services/languages/update/trkProviderlanguageUpdate.tpl.html',
                controller: 'editExistingLanguageDialogController',
                resolve: {
                    model: function () { return model; },
                    config: function () { return config; }
                }
            });

            return modalInstance.result;
        }
    };
    
    function languagesManager($http) {

        return {
            add: add,
            remove: remove,
            update: update
        };

        function remove(id) {
            var url = '/api/language/' + id;
            return $http.delete(url);
        }

        function add(model) {
            var url = 'api/providerService/' + model.serviceId + '/languages';
            return $http.post(url, model);
        }

        function update(model) {
            var url = 'api/providerLanguage/' + model.id;
            return $http.put(url, model);
        }
    }
})();
