import "./css/form.css"
import React, { useState } from 'react';
import { useHistory } from "react-router-dom"; 

function AddProject() {
    const [code, setCode] = useState("");
    const [subcodes, setSubcodes] = useState("");
    const [budget, setBudget] = useState("");

    let history = useHistory();

    async function addProject(evt) {
        evt.preventDefault();
        fetch(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/projects/create' , {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Code: code,
                    Subcodes: subcodes,
                    UserId: parseInt(localStorage.getItem("userId")),
                    Budget: budget
                })
            }
        )
        .then((res) => {
            console.log(res.status);
            if(res.status === 403){
                history.push("/exist");
            }else{
                history.push("/project");
            }
        });
       
    }

    return (
        <div class="form-box">
            <h2>Add project</h2>
            <form onSubmit={addProject}>
                <div class="user-box">
                    <input type="text" onChange={e => setCode(e.target.value)} required />
                    <label>Code</label>
                </div>
                <div class="user-box">
                    <input type="text" onChange={e => setSubcodes(e.target.value)}  required />
                    <label>Subcodes(separated by comma)</label>
                </div>
                <div class="user-box">
                    <input type="number" onChange={e => setBudget(e.target.value)} required />
                    <label>Budget</label>
                </div>
                <button class="form-button" type="submit">Submit</button>
            </form>

            <a class="form-button" href="/project">Cancel</a>
        </div>
    );
}

export default AddProject;