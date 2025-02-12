import React, { useRef, useEffect } from "react";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import "./RichTextEditor.css";

const RichTextEditor = ({ value, onChange, onFocus, onBlur, isExpanded }) => {
    const quillRef = useRef(null);

    useEffect(() => {
        if (quillRef.current) {
            const editor = quillRef.current.getEditor();
            if (editor.root.innerHTML !== value) {
                editor.root.innerHTML = value || "";
            }

            const quillElement = quillRef.current.getEditor().root;
            quillElement.addEventListener("focus", onFocus);
            quillElement.addEventListener("blur", onBlur); // Добавлено

            return () => {
                quillElement.removeEventListener("focus", onFocus);
                quillElement.removeEventListener("blur", onBlur); // Добавлено
            };
        }
    }, [value, onFocus, onBlur]);

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