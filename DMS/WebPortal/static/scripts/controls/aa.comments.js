var GetCommentsControl = function (containerClass, comments) {
    if (comments == null || comments == "undefined")
        return;

    if (comments.length == undefined || comments.length == null) {
        // New comment is added
        var html =
                "<div class=\"arrow_container\">" +
                    "<div class=\"left\">" +
                        "<div class=\"info\">" +
                            "<div class=\"commenter\">" +
                                "<div class=\"comment-name\">" + comments.CreatedBy + "</div>" +
                                "<div class=\"date-created\">" + comments.ProcessedDateCreated + "</div>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class=\"right\">" +
                        "<div class=\"comment\">" +
                            "<div class=\"arrow_box\">" +
                                "<div class=\"comment-content\">" +
                                    "<blockquote class=\"quote\">" + comments.UserComment + "</blockquote>" +
                                "</div>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class=\"clear\"></div>" +
                "</div>";

        console.log($(".arrow_container"));

        $(".previous-comments").find("div").each(function () {
            if ($(this).html() == "Current document does not have any associated comments. Please add new comments." ||
                $(this).html() == "Please add some comment to save for this document. Currently comments field is left empty") {
                $(this).remove();                
            }
        });

        $(html).insertBefore($(".previous-comments .arrow_container:first"));
    }
    else {
        // Load all previous comments
        comments.forEach(function (comment) {
            var html =
                "<div class=\"arrow_container\">" +
                    "<div class=\"left\">" +
                        "<div class=\"info\">" +
                            "<div class=\"commenter\">" +
                                "<div class=\"comment-name\">" + comment.CreatedBy + "</div>" +
                                "<div class=\"date-created\">" + comment.ProcessedDateCreated + "</div>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class=\"right\">" +
                        "<div class=\"comment\">" +
                            "<div class=\"arrow_box\">" +
                                "<div class=\"comment-content\">" +
                                    "<blockquote class=\"quote\">" + comment.UserComment + "</blockquote>" +
                                "</div>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class=\"clear\"></div>" +
                "</div>";

            $(".previous-comments").append(html);
        });
    }

    $("#user-comments").val("");
};

