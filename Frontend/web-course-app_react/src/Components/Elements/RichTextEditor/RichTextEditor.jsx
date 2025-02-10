import React, { useRef, useEffect } from "react";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import "./RichTextEditor.css";

const RichTextEditor = ({ value, onChange, onFocus, isExpanded }) => {
    const quillRef = useRef(null);

    useEffect(() => {
        if (quillRef.current) {
            const editor = quillRef.current.getEditor();
            if (editor.root.innerHTML !== value) {
                editor.root.innerHTML = value || "";
            }

            const quillElement = quillRef.current.getEditor().root;
            quillElement.addEventListener("focus", onFocus);

            return () => {
                quillElement.removeEventListener("focus", onFocus);
            };
        }
    }, [value, onFocus]);

    const modules = {
        toolbar: {
            container: [
                [{ header: [1, 2, 3, false] }],
                ["bold", "italic", "underline", "strike"],
                [{ list: "ordered" }, { list: "bullet" }],
                ["link", "image"],
                ["code-block"],
                ["clean"],
            ],
        },
    };

    const formats = [
        "header",
        "bold",
        "italic",
        "underline",
        "strike",
        "list",
        "bullet",
        "link",
        "image",
        "code-block",
    ];

    return (
        <div
            className={`rich-text-editor-container ${isExpanded ? "expanded" : ""}`}
            style={{ height: isExpanded ? "500px" : "300px" }}
        >
            <ReactQuill
                ref={quillRef}
                value={value || ""}
                onChange={onChange}
                modules={modules}
                formats={formats}
                theme="snow"
                style={{ height: "100%" }}
            />
        </div>
    );
};

export default RichTextEditor;