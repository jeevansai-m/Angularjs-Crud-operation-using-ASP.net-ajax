/// <reference path="c:\users\jm147\documents\visual studio 2015\Projects\Task\Task\scripts/angular.js" />
var app = angular.module("Task", []);
app.controller("ctrlRegister", function ($scope, $http) {
    $scope.Id = 0;
    // to clear the form
    $scope.Btnclear = function () {
        $scope.Id = 0;
        $scope.Name = "";
        $scope.Surname = "";
        $scope.Pnum = "";
        $scope.Age = "";
        $scope.Idtype = "";
        $scope.Proof = "";
        $scope.answers.Gender = "";
    };
    $scope.BtnSubmit = function () {
        $scope.reg = {};
        $scope.reg.Id = $scope.Id;
        $scope.reg.Name = $scope.Name;
        $scope.reg.Surname = $scope.Surname;
        $scope.reg.Pnum = $scope.Pnum;
        $scope.reg.Age = $scope.Age;
        $scope.reg.Idtype = $scope.Idtype;
        $scope.reg.Proof = $scope.Proof;
        $scope.reg.Gender = $scope.answers.Gender;
        $http({
            method: "post",
            url: "DataService.asmx/InsertRegister",
            datatype: "json",
            data: '{data: ' + JSON.stringify($scope.reg) + '}',
            contentType: "application/json; charset=utf-8"
        }).then(function successCallback(response) {
            inserted();

        }, function () {
            alert("Error Occur at insert button");
        });
    };
    function inserted() {
        if ($scope.Id == 0) {
            alert("inserted");
            $scope.TblDisplay();
            $scope.Btnclear();
        }
        else {
            alert("Updated");
            $scope.TblDisplay();
            $scope.Btnclear();
        }
    }
    //$scope.RegEdit = function (Id) {
    //    $http({
    //        url: "DataService.asmx/On_Click_EditRegister",
    //        method: 'Get',
    //        data: { ID: Id },
    //        dataType: "json",
    //    }).then(function (response) {
    //        alert('geting data');
    //        $scope.dataedit = response.data;
    //        $scope.Id = $scope.dataedit.Id;
    //        $scope.Name = $scope.dataedit.Name;
    //        $scope.Surname = $scope.dataedit.Surname;
    //        $scope.Pnum = $scope.dataedit.Pnum;
    //        $scope.Age = $scope.dataedit.Age;
    //        $scope.Idtype = $scope.dataedit.Idtype;
    //        $scope.Proof = $scope.dataedit.Proof;
    //        $scope.answers.Gender = $scope.dataedit.Gender;
    //    }, function () {
    //        alert("Error Occur at edit");
    //    })};
    $scope.RegEdit = function (reg) {
        $scope.dataedit = reg;
        $scope.Id = $scope.dataedit.Id;
        $scope.Name = $scope.dataedit.Name;
        $scope.Surname = $scope.dataedit.Surname;
        $scope.Pnum = $scope.dataedit.Pnum;
        $scope.Age = $scope.dataedit.Age;
        $scope.Idtype = $scope.dataedit.Idtype;
        $scope.Proof = $scope.dataedit.Proof;
        if ($scope.dataedit.Gender == "Female") {
            $scope.answers = { Gender: "Female" };
        }
        else {
            $scope.answers = { Gender: "Male" };
        }
        
    };

    $scope.RegDel = function (ID) {
        $http({
            method: "post",
            url: "DataService.asmx/Delete",
            datatype: "json",
            data: { ID: ID },
            contentType: "application/json; charset=utf-8"
        }).then(function (response) {
            alert('deleted suceesfully');
            $scope.TblDisplay();
        }, function () {
            alert("Error Occur");
        })
    };

    $scope.TblDisplay = function () {
        $http({
            method: "get",
            url: "DataService.asmx/GetRegister"
        }).then(function (response) {
            $scope.Registers = response.data;
        }, function () {
            alert("Error Occur");
        })
    };


});