'use strict';

angular.module('myApp.home', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: 'home/view.html',
            controller: 'HomeCtrl'
        });
    }])

    .controller('HomeCtrl', ['$scope', '$http', '$uibModal',
        function ($scope, $http, $uibModal) {
            let url = 'http://localhost:62080/api/vegetables';

            $scope.vegetables = [];
            $scope.searchById;

            $scope.fetch = function () {
                $http.get(url).then(function (response) {
                    $scope.vegetables = response.data;
                }, function (response) {
                    console.log(response);
                });
            };

            $scope.fetchById = function () {
                if ($scope.searchById) {
                    $scope.vegetables = [];
                    $http.get(url + '/' + $scope.searchById).then(function (response) {
                        $scope.vegetables.push(response.data);
                    }, function (response) {
                        console.log(response);
                    });
                }
            };

            $scope.delete = function (vegetable) {
                var modalOptions = {
                    templateUrl: 'home/confirmation.html',
                    controller: deleteModal,
                    keyboard: true,
                    resolve: {
                        vegetable: function () {
                            return vegetable;
                        }
                    }
                };

                $uibModal.open(modalOptions).result.then(function () {
                    let index = null;
                    angular.forEach($scope.vegetables, function (item, key) {
                        if (item.id === vegetable.id) index = key;
                    });
                    if (index !== null) $scope.vegetables.splice(index, 1);
                });
            };

            let deleteModal = ['$scope', '$uibModalInstance', '$http', 'vegetable',
                function ($scope, $uibModalInstance, $http, vegetable) {
                    $scope.working = false;

                    $scope.yes = function () {
                        $scope.working = true;

                        $http.delete(url + '/' + vegetable.id).then(function (response) {
                            if (response.data) {
                                $uibModalInstance.close('ok');
                            }
                            else {
                                $uibModalInstance.dismiss('cancel');
                            }
                        }, function (response) {
                            console.log(response);
                            $uibModalInstance.dismiss('cancel');
                        });
                    };
                    $scope.no = function () { $uibModalInstance.dismiss('cancel'); };
                }];

            $scope.update = function (vegetable) {
                var modalOptions = {
                    templateUrl: 'home/form.html',
                    controller: updateModal,
                    keyboard: true,
                    resolve: {
                        vegetable: function () {
                            return angular.copy(vegetable);
                        }
                    }
                };

                $uibModal.open(modalOptions).result.then(function (result) {
                    angular.forEach($scope.vegetables, function (item) {
                        if (item.id === vegetable.id) {
                            item.name = result.name;
                            item.price = result.price;
                        }
                    });
                });
            };

            let updateModal = ['$scope', '$uibModalInstance', '$http', 'vegetable',
                function ($scope, $uibModalInstance, $http, vegetable) {
                    $scope.working = false;
                    $scope.vegetable = vegetable;

                    $scope.submit = function () {
                        $scope.working = true;

                        $http.put(url + '/' + vegetable.id, $scope.vegetable).then(function (response) {
                            if (response.data) {
                                $uibModalInstance.close($scope.vegetable);
                            }
                            else {
                                $uibModalInstance.dismiss('cancel');
                            }
                        }, function (response) {
                            console.log(response);
                            $uibModalInstance.dismiss('cancel');
                        });
                    };
                    $scope.cancel = function () { $uibModalInstance.dismiss('cancel'); };
                }];

            $scope.add = function () {
                var modalOptions = {
                    templateUrl: 'home/form.html',
                    controller: addModal,
                    keyboard: true,
                    resolve: {
                        vegetable: function () {
                            return {
                                name: '',
                                price: 0.0
                            }
                        }
                    }
                };

                $uibModal.open(modalOptions).result.then(function (result) {
                    $scope.vegetables.unshift(result);
                });
            };

            let addModal = ['$scope', '$uibModalInstance', '$http', 'vegetable',
                function ($scope, $uibModalInstance, $http, vegetable) {
                    $scope.working = false;
                    $scope.vegetable = vegetable;

                    $scope.submit = function () {
                        $scope.working = true;

                        $http.post(url, $scope.vegetable).then(function (response) {
                            if (response.data) {
                                $uibModalInstance.close(response.data);
                            }
                            else {
                                $uibModalInstance.dismiss('cancel');
                            }
                        }, function (response) {
                            console.log(response);
                            $uibModalInstance.dismiss('cancel');
                        });
                    };
                    $scope.cancel = function () { $uibModalInstance.dismiss('cancel'); };
                }];

            $scope.fetch();
        }]);