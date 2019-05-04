$(document).ready(function () {

        //bind table with data
        LoadBooks();

        //add or edit book
        $(".btn-success").on("click", function (event) {
            debugger;
            //disable when clicked
            var _This = $(this);
            _This.attr("disable", "disable");

            var BookName = $('#bookname').val().trim(),
                Authorname = $('#authorname').val().trim(),
                BookNo = $('#bookno').val().trim(),
                BookType = $('#booktype').val().trim(),
                ID = $('#book_Id').val().trim();

            if (BookName == "") {
                alert('Book name required !!');
                return
            }

            else if (Authorname == "") {
                alert('Book Author required !!');
                return
            }

            else if (BookNo == "") {
                alert('Book numbers required !!');
                return
            }
            else if (BookType == "") {
                alert('Book Type required !!');
                return
            }
            else
            {
                $.getJSON("/Book/NewOrEditBook", { BookName: BookName, Authorname: Authorname, BookNo: BookNo, BookType: BookType, ID: ID }, function (data) {
                    if (data != "") {
                        var html = "";
                        if (data != "updated") {
                            var id = data.split('-')

                            html += "<tr class='main-data tr_" + id[1] + "' data-id='" + id[1] + "'>";
                            html += "<td>" + BookName + " </td>"
                            html += "<td>" + Authorname + " </td>"
                            html += "<td>" + BookNo + " </td>"
                            html += "<td>" + BookType + " </td>"
                            html += "<td> <a class='btn btn-primary' onclick='DisplayEditDatainForm(this)'>Edit</a> </td>"
                            html += "</tr>";

                            $(".table").find("tbody").prepend(html);

                            _This.removeAttr("disable"); //Enabl click again
                            alert('Book Added Successfully');

                            $('#bookname').val('');
                            $('#authorname').val('');
                            $('#bookno').val('');
                            $('#booktype').val('');
                            $('#book_Id').val('');

                        }
                        else //edit done
                        {
                            var rowId = $(".tr_" + ID + "");
                            rowId.find("td").eq(0).html(BookName);
                            rowId.find("td").eq(1).html(Authorname);
                            rowId.find("td").eq(2).html(BookNo);
                            rowId.find("td").eq(3).html(BookType);

                            _This.removeAttr("disable"); //enable Edit Button
                            _This.val('Save');
                            alert("Book Edited Successfully");

                            $('#bookname').val('');
                            $('#authorname').val('');
                            $('#bookno').val('');
                            $('#booktype').val('');
                            $('#book_Id').val('');
                        }
                    }
                });
            }


        });
    });

//LoadBooks
function LoadBooks()
{
    $.getJSON("/Book/LoadBooks", {}, function (data) {
        if (data != null)
        {
            var html = "";
            $.each(data, function (key, value) {
                html += "<tr class='main-data tr_" + value.ID + "' data-id='" + value.ID + "' >";
                html += "<td>" + value.Name + " </td>"
                html += "<td>" + value.AuthorName + " </td>"
                html += "<td>" + value.BookNo + " </td>"
                html += "<td>" + value.BookType + " </td>"
                html += "<td> <a class='btn btn-primary' onclick='DisplayEditDatainForm(this)' >Edit</a></td>"
                html += "</tr>";
            });

            $(".table").find("tbody").html(html);
        }
    })
}

//show data to edit
function DisplayEditDatainForm(obj)
{
    debugger;
    var _this = $(obj).closest(".main-data");
    $("#bookname").val(_this.find("td").eq(0).html());
    $("#authorname").val(_this.find("td").eq(1).html());
    $("#bookno").val(_this.find("td").eq(2).html().trim());
    $("#booktype").val(_this.find("td").eq(3).html());
    $("#book_Id").val(_this.attr("data-id"));

    //change Add Button
    $(".btn-success").val("Edit");
}

