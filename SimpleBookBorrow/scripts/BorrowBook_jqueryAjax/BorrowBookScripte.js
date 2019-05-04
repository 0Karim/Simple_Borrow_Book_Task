$(document).ready(function () {
    //bind table with data
    LoadAllBooks();

    //Load All Borrowes
    LoadAllBorrowers();

    //here will go the action of save
    //add or edit book
    $(".btn-success").on("click", function (event) {
        //disable when clicked
        var _This = $(this);
        _This.attr("disable", "disable");

        debugger;
        if ($('#BookID').val().trim() == "") {
            alert('Please Select a Book');
            return;
        }
        if ($('#BorrowerID').val().trim() == "") {
            alert('Please Select a Borrower');
            return;
        }

        var SelectedBook = $('#BookID').val().trim();
        var selectedBorrower = $('#BorrowerID').val().trim();

        $.getJSON("/BorrowBook/AddBorrow", { SelectedBook: SelectedBook, selectedBorrower: selectedBorrower }, function (data) {

            debugger;
            if (data != "" && data == "Added") {
                LoadAllBooks();
                LoadAllBorrowers();

                _This.removeAttr("disable"); //Enabl click again
                alert('Book Added Successfully');

                $('#BookID').val('');
                $('#BorrowerID').val('');
            }
            else {
                alert('There is no books to borrow');
                $('#BookID').val('');
                $('#BorrowerID').val('');
                return;
            }

        });
    });

});



function LoadAllBorrowers() {
    $.getJSON("/BorrowBook/LoadAllBorrowers", {}, function (data) {
        x = data;
        debugger;
        if (data != null) {
            //debugger;
            var html = "";
            $.each(data, function (key, value) {
                html += "<tr class='main-data tr_" + value.ID + "' data-id='" + value.ID + "' >";
                html += "<td>" + value.Key + " </td>"
                html += "<td>" + value.Value + " </td>"
                html += "</tr>";
            });
            $("#bookborrowtable").find("tbody").html(html);
        }
    });
}



//LoadBooks
function LoadAllBooks() {
    //debugger;
    $.getJSON("/BorrowBook/LoadAllBooks", {}, function (data) {
        if (data != null) {

            var html = "";
            $.each(data, function (key, value) {
                html += "<tr class='main-data tr_" + value.ID + "' data-id='" + value.ID + "' >";
                html += "<td>" + value.Name + " </td>"
                html += "<td>" + value.BookNo + " </td>"
                html += "</tr>";
            });

            $("#book").find("tbody").html(html);
        }
    });
}