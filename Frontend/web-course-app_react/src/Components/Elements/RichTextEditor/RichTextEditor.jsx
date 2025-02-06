import React, { useRef } from "react";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import "./RichTextEditor.css"

const RichTextEditor = ({ value, onChange }) => {
    const quillRef = useRef(null);
    
    const modules = {
        toolbar: {
            container: [
                [{ header: [1, 2, 3, false] }],
                ["bold", "italic", "underline", "strike"],
                [{ list: "ordered" }, { list: "bullet" }],
                ["link", "image"],
                ["code-block"],
                ["clean"],
            ]
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
        <div style={{ height: "300px", marginBottom: "20px"}}>
            <ReactQuill
                ref={quillRef}
                value={value}
                onChange={onChange}
                modules={modules}
                formats={formats}
                theme="snow"
                style={{ height: "100%", maxWidth: "100%" }}
            />
        </div>
    );
};

export default RichTextEditor;