import { Route, Routes } from 'react-router-dom';

import './App.css';

import Header from './components/Header/Header';
import Login from './components/Login/Login';
import Footer from './components/Footer/Footer';
import Register from './components/Register/Register';
import Home from './components/Home/Home';

function App() {
    return (
        <>
            <Header />

            <main className="main-content">
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                </Routes>
            </main>

            <Footer />
        </>
    );
}

export default App;
