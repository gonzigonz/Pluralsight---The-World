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
		vm.newStop = {};

		var url = "/api/trips/" + vm.tripName + "/stops";

		$http.get(url)
			.then(function (res) {
				// Success
				angular.copy(res.data, vm.stops);
				_showMap(vm.stops);
			}, function (err) {
				// Failure
				vm.errorMessage = "Error Listing Stops (Server: " + err.data + ")";
			})
			.finally(function () {
				vm.isBusy = false
			});

		vm.addStop = function () {

			vm.isBusy = true;

			$http.post(url, vm.newStop)
				.then(function (res) {
					// Success
					vm.stops.push(res.data)
					_showMap(vm.stops);
					vm.newStop = {};
				}, function (err) {
					// Failure
					vm.errorMessage = "Add Request Failed (Server: " + err.data + ")";
				})
				.finally(function () {
					vm.isBusy = false;
				});
		}
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