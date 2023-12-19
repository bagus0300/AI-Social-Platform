import { useEffect, useState } from "react";
import styles from "./Userprofile.css";
import * as userService from "../../core/services/userService";
import { Link, useNavigate, useParams } from "react-router-dom";
import { useFormik } from "formik";
import { PATH, ProfileFormKeys } from "../../core/environments/costants";
import userProfileValidation from "./userProfileValidation";

export default function Userprofileedit() {
  const { userId } = useParams();
  const [userData, setUserData] = useState({});
  const [error, setError] = useState(null);

  useEffect(() => {
    userService
      .getUserData(userId)
      .then((result) => {
        setUserData(result);
      })
      .catch((error) => console.log(error));
  }, []);
  if (!userData) {
    return <div>Loading...</div>;
  }

  const initialValues = {
    [ProfileFormKeys.FirstName]: userData.firstName,
    [ProfileFormKeys.LastName]: userData.lastName,
    [ProfileFormKeys.Email]: "test@test.com",
    [ProfileFormKeys.PhoneNumber]: "12345678",
    [ProfileFormKeys.Country]: "bgbg",
    [ProfileFormKeys.State]: "sfsf",
    [ProfileFormKeys.Gender]: 1,
    [ProfileFormKeys.School]: "abc",
    [ProfileFormKeys.Birthday]: "2023-12-17T16:29:38.153Z",
    [ProfileFormKeys.Relationship]: 1,
    [ProfileFormKeys.Schools]: [
      {
        name: "string",
        state: "string",
      },
    ],
  };

  const {
    values,
    errors,
    isSubmitting,
    touched,
    handleChange,
    handleBlur,
    handleSubmit,
  } = useFormik({
    initialValues,
    enableReinitialize: true,
    onSubmit,
    validationSchema: userProfileValidation,
  });

  const updateSubmitHandler = async ({
    firstName,
    lastName,
    phoneNumber,
    country,
    state,
    gender,
    school,
    birthday,
    relationship,
    schools,
  }) => {
    await userService.update({
      firstName,
      lastName,
      phoneNumber,
      country,
      state,
      gender,
      school,
      birthday,
      relationship,
      schools,
    });

    // setAuth(result);

    // localStorage.setItem('accessToken', result.token);

    useNavigate(PATH.home);
  };

  async function onSubmit(values) {
    debugger;
    try {
      await updateSubmitHandler(values);
    } catch (error) {
      console.log("Error:", error);
    }
  }
  return (
    <div className="user-profile">
      <article className="post-item">
        <form onSubmit={handleSubmit}>
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
              <div className="username-profile">
                <label htmlFor={ProfileFormKeys.FirstName}></label>
                <input
                  className={
                    errors[ProfileFormKeys.FirstName] &&
                    touched[ProfileFormKeys.FirstName]
                      ? styles["name-input-error"]
                      : styles["name-input"]
                  }
                  type="text"
                  name={ProfileFormKeys.FirstName}
                  id={ProfileFormKeys.FirstName}
                  placeholder="First name"
                  onChange={handleChange}
                  onBlur={handleBlur}
                  value={values[ProfileFormKeys.FirstName]}
                />

                {errors[ProfileFormKeys.FirstName] &&
                  touched[ProfileFormKeys.FirstName] && (
                    <p className={styles["error-message"]}>
                      {errors[RegisterFormKeys.FirstName]}
                    </p>
                  )}

                <div className={styles["last-name"]}>
                  <label htmlFor={ProfileFormKeys.LastName}></label>
                  <input
                    className={
                      errors[ProfileFormKeys.LastName] &&
                      touched[ProfileFormKeys.LastName]
                        ? styles["name-input-error"]
                        : styles["name-input"]
                    }
                    type="text"
                    name={ProfileFormKeys.LastName}
                    id={ProfileFormKeys.LastName}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values[ProfileFormKeys.LastName]}
                  />

                  {errors[ProfileFormKeys.LastName] &&
                    touched[ProfileFormKeys.LastName] && (
                      <p className={styles["error-message"]}>
                        {errors[ProfileFormKeys.LastName]}
                      </p>
                    )}
                </div>
              </div>
            </div>
            <section className={styles["phone-number-wrapper"]}>
              <label htmlFor={ProfileFormKeys.PhoneNumber}></label>
              <input
                className={
                  errors[ProfileFormKeys.PhoneNumber] &&
                  touched[ProfileFormKeys.PhoneNumber]
                    ? styles["input-field-error"]
                    : styles["input-field"]
                }
                type="text"
                name={ProfileFormKeys.PhoneNumber}
                id={ProfileFormKeys.PhoneNumber}
                placeholder="Phone number"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[ProfileFormKeys.PhoneNumber]}
              />

              {errors[ProfileFormKeys.Email] &&
                touched[ProfileFormKeys.Email] && (
                  <p className={styles["error-message"]}>
                    {errors[ProfileFormKeys.Email]}
                  </p>
                )}
            </section>
            <section className={styles["phone-number-wrapper"]}>
              <label htmlFor={ProfileFormKeys.Country}></label>
              <input
                className={
                  errors[ProfileFormKeys.Country] &&
                  touched[ProfileFormKeys.Country]
                    ? styles["input-field-error"]
                    : styles["input-field"]
                }
                type="text"
                name={ProfileFormKeys.Country}
                id={ProfileFormKeys.Country}
                placeholder="Country"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[ProfileFormKeys.Country]}
              />

              {errors[ProfileFormKeys.Country] &&
                touched[ProfileFormKeys.Country] && (
                  <p className={styles["error-message"]}>
                    {errors[ProfileFormKeys.Country]}
                  </p>
                )}
            </section>
          </div>
          <button
            type="submit"
            className={styles["register-button"]}
            disabled={isSubmitting}
          >
            Update
          </button>
        </form>
      </article>
    </div>
  );
}
