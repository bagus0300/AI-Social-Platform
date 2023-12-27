import { useState, useEffect } from "react";
import styles from "./Userprofile.css";
import * as userService from "../../core/services/userService";
import { Link, useParams } from "react-router-dom";
import { PATH } from "../../core/environments/costants";
import { pathToUrl } from "../Userprofile/pathUtils";

export default function Userprofile() {
  const { userId } = useParams();

  const [userData, setUserData] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    userService
      .getUserDetails(userId)
      .then((result) => {
        setUserData(result);
      })
      .catch((error) => setError(error));
  }, [userId]);

  if (error) {
    return <div>{error}</div>;
  }

  if (!userData) {
    return <div>Loading...</div>;
  }

  return (
    <div className="user-profile">
      <article className="post-item">
        <img
          className="user-cover"
          src="../../../public/images/iceage.png"
          alt="User cover photo"
        />
        <div className="user-info-wrapper">
          <img
            className="user-img"
            src="../../../public/images/mamut.jpg"
            alt="User profile pic"
          />
          <div className="user-info-text">
            <p className="username-profile">
              {userData.firstName} {userData.lastName}
            </p>
            <p className="posted-user">
              E-mail:
              <a href="mailto: {userData?.email}"> {userData?.userName}</a>
            </p>
            <p>GSM: {userData.phoneNumber}</p>
          </div>

          <div className="edit">
            <Link to={pathToUrl(PATH.profileedit, { userId: userId })}>
              <img
                className="edit-img"
                src="../../../public/images/edit-pen-icon-6.jpg"
                alt="edit button"
              />
            </Link>
          </div>
        </div>
        <section className="content-description">
          <p>
            City, Country: {userData.state}, {userData.country}
          </p>
          <p>Schools: {userData.userSchools}</p>
          <p>
            Birthday: {userData.birthday}, Gender: {userData.gender}
          </p>
          <p>Relationship Status: {userData.relationship}</p>

          <p>
            <a href="#">Publications</a>
          </p>
          <p>
            <a href="#">Friends</a>
          </p>
        </section>
      </article>
    </div>
  );
}
