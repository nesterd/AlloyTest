import React from 'react';

export default function Errors(props){    
    return(
        props.errors && props.errors.length > 0 ?
         <div className="alert alert-danger">
             <ul>
             {props.errors.map((error, i) =>
                <li key={'_' + i}>{error}</li>)}
             </ul>
        </div>
        :null
    );
}