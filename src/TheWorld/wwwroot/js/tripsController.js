// tripsController.js
(function () {

	"use strict";

	angular.module("app-trips").controller("tripsController", tripController);

	function tripController($http) {

		var vm = this;

		vm.trips = [];

		vm.newTrip = {};

		vm.errorMessage = "";
		vm.isBusy = true;

		$http.get("/api/trips")
			.then(function (res) {
				// Success
				angular.copy(res.data, vm.trips);
			}, function (error) {
				// Failure
				vm.errorMessage = "Failed to load data: " + error;
			})
			.finally(function () {
				 vm.isBusy = false;
			}); 

		vm.addTrip = function () {

			vm.isBusy = true;
			vm.errorMessage = "";

			$http.post("/api/trips", vm.newTrip)
			.then(function (res) {
				// Sucess
				vm.trips.push(res.data);
				vm.newTrip = {};
			}, function (error) {
				// Failure
				vm.errorMessage = "Failed to save new trip: " + error;
			})
			.finally(function () {
				vm.isBusy = false;
			})
		};

	}

})();