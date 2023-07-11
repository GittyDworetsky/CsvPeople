import React, { useState } from "react";
import axios from "axios";

const Generate = () => {

    const [amount, setAmount] = useState(0);

    const onGenerateClick = async () => {

        window.location = `/api/csv/generatepeoplecsv?amount=${ amount }`;
        
            
    }
    return (
        <div className="d-flex w-100 justify-content-center align-self-center">
            <div className="row">
                <input onChange={(e) => setAmount(e.target.value)} type="text" className="form-control-lg" placeholder="Amount" />
            </div>
            <div className="row">
                <div className="col-md-4 offset-md-2">
                    <button onClick={onGenerateClick} className="btn btn-primary btn-lg">Generate</button>
                </div>
            </div>
        </div>

    )

}

export default Generate;