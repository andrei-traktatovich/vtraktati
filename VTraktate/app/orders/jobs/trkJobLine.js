(function () {
    angular.module('orders')
	.directive('trkJobLine', trkJobLine);
	
	function trkJobLine() {
		return {
			restrict: 'EA',
			scope: true, 
			// expect there to be $scope.job, $scope.orgerOptions, $scope.order ... 
			templateUrl: 'app/orders/jobs/trkJobLine.tpl.html',
			controller: 'jobLineController'
		}
	}
})(); 