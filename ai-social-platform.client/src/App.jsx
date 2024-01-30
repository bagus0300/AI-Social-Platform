import { Route, Routes } from 'react-router-dom';

import { PATH } from './core/environments/costants';
import { AuthProvider } from './contexts/authContext';
import './App.css';

import AuthGuard from './guards/AuthGuard';
import LoggedInGuard from './guards/LoggedInGuard';
import ErrorBoundary from './guards/ErrorBoundary/ErrorBoundary';

import Header from './components/Header/Header';
import Login from './components/Login/Login';
import Footer from './components/Footer/Footer';
import Register from './components/Register/Register';
import Home from './components/Home/Home';
import Logout from './components/Logout/Logout';
import Userprofile from './components/Userprofile/Userprofile';
import Userprofileedit from './components/Userprofile/Userprofileedit';
import CreatePost from './components/CreatePost/CreatePost';
// import Successfully from './components/Successfully/Successfully';
import NotFound from './components/NotFound/NotFound';
import PostDetails from './components/PostDetails/PostDetails';
import Postlist from './components/PostEdit/PostList';
import Postedit from './components/PostEdit/Postedit';
import Search from './components/Search/Search';
import LeftAside from './components/LeftAside/LeftAside';
import RightAside from './components/RightAside/RightAside';

function App() {
    return (
        <ErrorBoundary>
            <AuthProvider>
                <>
                    <Header />
                    <div className="main-wrapper">
                        <main className="main-content">
                            <Routes>
                                <Route
                                    path={PATH.notFound}
                                    element={<NotFound />}
                                />

                                <Route element={<LoggedInGuard />}>
                                    <Route
                                        path={PATH.login}
                                        element={<Login />}
                                    />
                                    <Route
                                        path={PATH.register}
                                        element={<Register />}
                                    />
                                </Route>

                                <Route element={<AuthGuard />}>
                                    <Route
                                        path={PATH.home}
                                        element={<Home />}
                                    />
                                    <Route
                                        path={PATH.search}
                                        element={<Search />}
                                    />
                                    <Route
                                        path={PATH.logout}
                                        element={<Logout />}
                                    />
                                    <Route
                                        path={PATH.create}
                                        element={<CreatePost />}
                                    />
                                    <Route
                                        path={PATH.detailsPost}
                                        element={<PostDetails />}
                                    />
                                    {/* <Route
                                    path={PATH.successfully}
                                    element={<Successfully />}
                                /> */}

                                    <Route
                                        path={PATH.profile}
                                        element={<Userprofile />}
                                    />
                                    <Route
                                        path={PATH.profileedit}
                                        element={<Userprofileedit />}
                                    />
                                </Route>
                                <Route
                                    path={PATH.postlist}
                                    element={<Postlist />}
                                />
                                <Route
                                    path={PATH.postedit}
                                    element={<Postedit />}
                                />
                            </Routes>
                        </main>

                        <LeftAside />

                        <RightAside />
                    </div>
                    <Footer />
                </>
            </AuthProvider>
        </ErrorBoundary>
    );
}

export default App;
