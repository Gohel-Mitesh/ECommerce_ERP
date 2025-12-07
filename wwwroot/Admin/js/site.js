// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//-------For Searchable DropDown -------//
$(document).ready(function () {
    $('.select2').select2({
        width: '100%', // Ensures it uses the full width of the parent container
        dropdownCssClass: "all", // Add a custom class to the dropdown            
    });

    $(".select2").on("select2:open", function () {
        let searchField = document.querySelector('.select2-container .select2-search__field');
        if (searchField) {
            searchField.focus();
        }
    });
});
//-------For Searchable DropDown -------//