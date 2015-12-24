// tripEditorController.js
(function () {
	'use strict';

	angular.module('app-trips').controller('tripEditorController', tripEditorController);

	function tripEditorController($routeParams, $http) {
		var vm = this;

		vm.tripName = $routeParams.tripName;
		vm.stops = [];
		vm.errorMessage = "";
		vm.isBusy = true;

		$http.get("/api/trips/" + vm.tripName + "/stops")
			.then(function (res) {
				// Success
				angular.copy(res.data, vm.stops)
			}, function (err) {
				// Failure
				vm.errorMessage = "Failed to load stops: " + err;
			})
			.finally(function () {
				vm.isBusy = false
			});
	};

})();