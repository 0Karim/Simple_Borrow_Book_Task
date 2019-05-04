$(document).ready(function () {

    //bind table with data

    LoadBorrowers();

    //add or edit book
    $(".btn-success").on("click", function (event) {
        debugger;
        //disable when clicked
        var _This = $(this);
        _This.attr("disable", "disable");

        var radioResult = $('input[type="radio"]:checked');
        if (radioResult.length < 0 || radioResult.length == 0) //there is a check value
        {
            alert('You should choose your gender!!');
            return;
        }

        var test = radioResult.val().trim();

        var BorrowerFirstName = $('#firstname').val().trim(),
            BorrowerLastName = $('#lastname').val().trim(),
            MobileNo = $('#mobileno').val().trim(),
            Gender = radioResult.val().trim() == "Male" ? true : false,
            ID = $('#borrower_Id').val().trim();

        if (BorrowerFirstName == "") {
            alert('Firstname required !!');
            return
        }

        else if (BorrowerLastName == "") {
            alert('Lastname required !!');
            return
        }

        else if (MobileNo == "") {
            alert('Mobile numbers required !!');
            return
        }
        else {
            $.getJSON("/Borrower/NewOrEditBorrower", { BorrowerFirstName: BorrowerFirstName, BorrowerLastName: BorrowerLastName, MobileNo: MobileNo, Gender: Gender, ID: ID }, function (data) {
                if (data != "") {
                    var html = "";
                    if (data != "updated") {
                        var id = data.split('-')

                        html += "<tr class='main-data tr_" + id[1] + "' data-id='" + id[1] + "'>";
                        html += "<td>" + BorrowerFirstName + " </td>"
                        html += "<td>" + BorrowerLastName + " </td>"
                        html += "<td>" + MobileNo + " </td>"
                        html += "<td>" + radioResult.val().trim() + " </td>"
                        html += "<td> <a class='btn btn-primary' onclick='DisplayEditDatainForm(this)'>Edit</a> </td>"
                        html += "</tr>";

                        $("#borrowtable").find("tbody").prepend(html);

                        _This.removeAttr("disable"); //Enabl click again
                        alert('Borrow Added Successfully');

                        $('#firstname').val('');
                        $('#lastname').val('');
                        $('#mobileno').val('');
                        radioResult.val('');
                        $('#borrower_Id').val('');

                    }
                    else //edit done
                    {
                        var rowId = $(".tr_" + ID + "");
                        rowId.find("td").eq(0).html(BorrowerFirstName);
                        rowId.find("td").eq(1).html(BorrowerLastName);
                        rowId.find("td").eq(2).html(MobileNo);
                        rowId.find("td").eq(3).html(radioResult.val().trim());

                        _This.removeAttr("disable"); //enable Edit Button
                        _This.val('Save');
                        alert("Borrow Edited Successfully");

                        $('#firstname').val('');
                        $('#lastname').val('');
                        $('#mobileno').val('');
                        radioResult.val('');
                        $('#borrower_Id').val('');
                    }
                }
            });
        }


    });
});


//LoadBorrowers
function LoadBorrowers() {
    debugger;
    $.getJSON("/Borrower/LoadBorrowers", {}, function (data) {
        if (data != null) {
            debugger;
            var html = "";
            $.each(data, function (key, value) {
                html += "<tr class='main-data tr_" + value.ID + "' data-id='" + value.ID + "' >";
                html += "<td>" + value.Firsname + " </td>"
                html += "<td>" + value.Lastname + " </td>"
                html += "<td>" + value.MobileNo + " </td>"

                var gender = '';
                if (value.Gender == true) {
                    gender = 'Male';
                } else {
                    gender = 'Female';
                }
                html += "<td>" + gender.trim() + " </td>"
                html += "<td> <a class='btn btn-primary' onclick='DisplayEditDatainForm(this)' >Edit</a></td>"
                html += "</tr>";
            });

            $("#borrowtable").find("tbody").html(html);
        }
    })
}

//show data to edit
function DisplayEditDatainForm(obj) {
    debugger;
    var _this = $(obj).closest(".main-data");
    $("#firstname").val(_this.find("td").eq(0).html());
    $("#lastname").val(_this.find("td").eq(1).html());
    $("#mobileno").val(_this.find("td").eq(2).html().trim());
    _this.find("td").eq(3).html().trim() == 'Male' ? $('#male').attr('checked', 'checked') : $('#female').attr('checked', 'checked')
    $("#borrower_Id").val(_this.attr("data-id"));

    //change Add Button
    $(".btn-success").val("Edit");
}
