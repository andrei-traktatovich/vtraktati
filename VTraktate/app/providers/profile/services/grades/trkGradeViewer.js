(function () {
    // this is a temporary solution.
    // it assumes that only old-style grades exist ...
    angular.module('app')
    .directive('trkGradeViewer', trkGradeViewer)
    .controller('trkGradeViewerController', trkGradeViewerController);

    function trkGradeViewer() {
        return {
            scope: {
                languageId: '='
            },
            templateUrl: 'app/providers/profile/services/grades/trkGradeViewer.tpl.html',
            controller: 'trkGradeViewerController'
        };
    }

    function trkGradeViewerController($scope, $http, toastr) {
        $scope.showing = false;
        $scope.show = show;
        $scope.hide = hide;
        $scope.domainText = domainText;

        var resolved = false;

        function domainText(item) {
            if (item.primaryDomainName != item.secondaryDomainName)
                return item.primaryDomainName + ', ' + item.secondaryDomainName;
            else
                if (item.primaryDomainName != '-')
                    return item.primaryDomainName;
                else return item.secondaryDomainName;
        }

        function hide() {
            $scope.showing = false;
        }

        function show() {
            var url = "api/serviceGrades/" + $scope.languageId;
            if (!resolved) {
                $http.get(url)
                    .success(updateView)
                    .error(displayError);
            }
            else
                $scope.showing = true;
            
            function displayError() {
                toastr.error('Ошибка');
            }

            function updateView(data) {
                $scope.showing = true;
                $scope.items = data;
                resolved = true;
            }
        }
    }
})();