import "./css/form.css"
import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { useHistory } from "react-router-dom"; 
import moment from "moment";

function AddActivity() {
    const [date, setDate] = useState("");
    const [projects, setProjects] = useState("");
    const [code, setCode] = useState("");
    const [subprojects, setSubprojects] = useState("");
    const [subcode, setSubcode] = useState("");
    const [description, setDescription] = useState("");
    const [time, setTime] = useState("");

    let history = useHistory();
    useEffect(() => {

        setDate(moment().format("YYYY-MM-DD"));
        async function loadProjects() {
            
            const projects = await axios(
                process.env.REACT_APP_BACKEND_BASE_URL + '/api/projects/all'
            );
            setProjects(projects.data);
            setCode(projects.data[0].code);
          }
          loadProjects();
          
          
        
    }, []);

    async function loadCode(e) {
        setCode(e.target.value);
        const response = await axios(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/projects/subprojects/' + e.target.value
        );
        setSubprojects(response.data);
    }

    async function addActivity(evt) {
        evt.preventDefault();
        fetch(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/activities/create' , {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Date: date,
                    Code: code,
                    Subcode: subcode,
                    UserId: parseInt(localStorage.getItem("userId")),
                    Description: description,
                    Time: time
                })
            }
        )
        .then((res) => {
            if(res.status === 200){
                history.goBack();
            }else{
                history.push("/close");
            }
        });
       
    }
    async function cancel() {
        history.goBack();
    }

    return (
        <div class="form-box">
            <h2>Add activity</h2>
            <form onSubmit={addActivity}>
                <label>Date</label>
                <div class="user-box">
                    <input class="date text-box single-line" type="date" onChange={e => setDate(e.target.value)} defaultValue={moment().format("YYYY-MM-DD")} required />
                </div>
                <label>Code</label>
                <div class="select-box">
                    <select onChange={loadCode}>
                        {projects && projects.map(project => (
                            <option key={project.code} value={project.code}>
                                {project.code}
                            </option>
                        ))}
                    </select>
                </div>
                <label>Subcode</label>
                <div class="select-box">
                    <select onChange={e => setSubcode(e.target.value)}>
                    {subprojects && subprojects.map(subproject => (
                            <option key={subproject.code} value={subproject.code}>
                                {subproject.code}
                            </option>
                        ))}
                    </select>
                </div>
                <div class="user-box">
                    <input type="number" onChange={e => setTime(e.target.value)} required />
                    <label>Time</label>
                </div>
                <div class="user-box">
                    <input type="text"  onChange={e => setDescription(e.target.value)}/>
                    <label>Description</label>
                </div>
                <button class="form-button" type="submit">Submit</button>
            </form>
            <button class="form-button" onClick={cancel}>Cancel</button>
        </div>
    );
}

export default AddActivity;