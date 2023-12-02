import { Route, Routes } from 'react-router-dom';

import './App.css';

import Header from './components/Header/Header';
import Login from './components/Login/Login';
import Footer from './components/Footer/Footer';
import Register from './components/Register/Register';
import Home from './components/Home/Home';
import { AuthProvider } from './contexts/authContext';
import { PATH } from './core/environments/costants';

function App() {
    return (
        <AuthProvider>
            <>
                <Header />

                <main className="main-content">
                    <Routes>
                        <Route path={PATH.home} element={<Home />} />
                        <Route path={PATH.login} element={<Login />} />
                        <Route path={PATH.register} element={<Register />} />
                    </Routes>
                </main>

                <Footer />
            </>
        </AuthProvider>
    );
}

export default App;
