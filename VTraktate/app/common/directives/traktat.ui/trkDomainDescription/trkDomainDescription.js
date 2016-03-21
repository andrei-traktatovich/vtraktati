(function() {
    angular.module('traktat.ui')
    .directive('trkDomainDescription', trkDomainDescription); 

    function trkDomainDescription() {
        
        return {
            restrict: 'EA',
            scope: {
                domain1: '=',
                domain2: '=',
                template: '<span>{{domain1}}, {{ domain2 }}{{domainText}}!</span>',
                controller: function ($scope) {
                    $scope.domainText = makeDomainText($scope.domain1, $scope.domain2);
                }
            }
        }

        function makeDomainText(domain1, domain2) {
            if (domain1 != domain2)
                return domain1 + ', ' + domain2;
            else
                if (domain1 != '-')
                    return domain1;
                else return domain2;
        }
    }

})