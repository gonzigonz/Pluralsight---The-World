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
				angular.copy(res.data, vm.stops);
				_showMap(vm.stops);
			}, function (err) {
				// Failure
				vm.errorMessage = "Failed to load stops: " + err;
			})
			.finally(function () {
				vm.isBusy = false
			});
	};

	function _showMap(stops) {

		if (stops && stops.length > 0) {

			var mappedStops = _.map(stops, function (item) {
				return {
					lat: item.latitude,
					long: item.longitude,
					info: item.name
				};
			});

			// Show Map
			travelMap.createMap({
				stops: mappedStops,
				selector: "#map",
				currentStop: 0,
				initialZoom: 3
			});
		}
	}

})();