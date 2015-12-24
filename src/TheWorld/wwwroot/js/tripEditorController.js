// tripEditorController.js
(function () {
	'use strict';

	angular.module('app-trips').controller('tripEditorController', tripEditorController);

	function tripEditorController($routeParams) {
		var vm = this;

		vm.tripName = $routeParams.tripName;
		vm.stops = [];
		vm.errorMessage = "";
		vm.isBusy = true;
	};

})();