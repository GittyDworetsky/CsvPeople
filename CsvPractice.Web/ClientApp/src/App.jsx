import React from 'react';
import './App.css';
import { Route, Routes } from 'react-router-dom';
import Layout from './Components/Layout';
import Home from './Pages/Home';
import Generate from './Pages/Generate';
import Upload from './Pages/Upload';

const App = () => {

    return (
        <Layout>
            <Routes>
                <Route exact path='/' element={<Home />} />
                <Route exact path='/generate' element={<Generate />} />
                <Route exact path='/upload' element={<Upload />} />
            </Routes>
        </Layout>
    )

};

export default App;