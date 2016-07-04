//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('groupService', ['$scope', '$http', '$controller', '$timeout', 'groupService',
  function ($scope, $http, $controller, $timeout, groupService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.service = groupService;

      $scope.formViewTemplate = '/Angular/Views/Admin/GroupService/detail.html';

      $scope.dataFilter = {
          name: '',
          currentRecord: 0,
          numberRecord: $scope.listNumberRow[0],
          currentPage: 1,
          propertyNameSort: 'ID',
          descending: true
      };

      $scope.loadDataForm = function (data) {
          groupService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listService;

              $scope.openModal('/Angular/Views/Admin/GroupService/edit.html');

          });
      };

      //$scope.loadPropertyFilter();
      $scope.loadData();

  }]);
var useStrict = 'use strict';
web365app.controller('groupServiceModified', ['$scope', '$http', '$controller', 'groupService',
  function ($scope, $http, $controller, groupService) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          for (var instanceName in CKEDITOR.instances) {
              CKEDITOR.instances[instanceName].updateElement();
          }

          $scope.dataForm = $(form.target).serialize();

          groupService.modified($scope.dataForm).then(function (res) {

              $scope.resetList();

              $scope.cancel();
          });
      };

  }]);