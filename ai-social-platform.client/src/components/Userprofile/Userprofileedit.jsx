import { useEffect, useState } from 'react';
import styles from './Userprofile.css';
import * as userService from '../../core/services/userService';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { useFormik, Field } from 'formik';
import { PATH, ProfileFormKeys } from '../../core/environments/costants';
import userProfileValidation from './userProfileValidation';

export default function Userprofileedit() {
    const { userId } = useParams();
    const [userData, setUserData] = useState({});
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        userService
            .getUserDetails(userId)
            .then((result) => {
                const updatedUserData = mapNullValuesToEmptyString(result);
                setUserData(updatedUserData);
            })
            .catch((error) => console.log(error));
    }, []);
    if (!userData) {
        return <div>Loading...</div>;
    }

    const mapNullValuesToEmptyString = (data) => {
        const updatedData = {};
        for (const key in data) {
            if (data.hasOwnProperty(key)) {
                updatedData[key] = data[key] === null ? '' : data[key];
            }
        }
        return updatedData;
    };

    let formattedBirthday = '';
    if (userData.birthday) {
        const date = new Date(userData.birthday);
        const month = (date.getMonth() + 1).toString().padStart(2, '0');
        const day = date.getDate().toString().padStart(2, '0');
        const year = date.getFullYear();
        formattedBirthday = `${year}-${month}-${day}`;
    }

    const initialValues = {
        [ProfileFormKeys.FirstName]: userData.firstName,
        [ProfileFormKeys.LastName]: userData.lastName,
        [ProfileFormKeys.PhoneNumber]: userData.phoneNumber,
        [ProfileFormKeys.ProfilePicture]: userData.profilePictureData,
        [ProfileFormKeys.CoverPhoto]: userData.coverPhotoData,
        [ProfileFormKeys.Country]: userData.country,
        [ProfileFormKeys.State]: userData.state,
        [ProfileFormKeys.Gender]:
            userData.gender === 'Man'
                ? '0'
                : userData.gender === 'Woman'
                ? '1'
                : userData.gender,
        [ProfileFormKeys.School]: userData.school,
        [ProfileFormKeys.Birthday]: formattedBirthday,
        [ProfileFormKeys.Relationship]:
            userData.relationship === 'Single'
                ? '0'
                : userData.relationship === 'InARelationship'
                ? '1'
                : userData.relationship === 'Married'
                ? '2'
                : userData.relationship,
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
        validationSchema: userProfileValidation,
        onSubmit,
    });

    const [profilePictureUrlCurrent, setProfilePictureUrlCurrent] =
        useState('');

    useEffect(() => {
        if (userData.profilePictureUrl) {
            const imageUrl = `${userData.profilePictureUrl}?${Math.random()}`;

            setProfilePictureUrlCurrent(imageUrl);
        } else {
            setProfilePictureUrlCurrent(
                '../../../public/images/default-profile-pic.png'
            );
        }
    }, [userData.profilePictureUrl]);

    const [coverPhotoUrlCurrent, setCoverPhotoUrlCurrent] = useState('');

    useEffect(() => {
        if (userData.coverPhotoUrl) {
            const imageUrlCover = `${userData.coverPhotoUrl}?${Math.random()}`;

            setCoverPhotoUrlCurrent(imageUrlCover);
        } else {
            setCoverPhotoUrlCurrent('../../../public/images/Logo.png');
        }
    }, [userData.coverPhotoUrl]);

    const handleChangeProfilePicture = (event) => {
        const selectedFile = event.target.files[0];

        if (selectedFile) {
            // Създаване на временен URL от избрания файл и показване на изображението в реално време
            setProfilePictureUrlCurrent(URL.createObjectURL(selectedFile));
        }
    };
    const handleChangeCoverPhoto = (event) => {
        const selectedFile = event.target.files[0];

        if (selectedFile) {
            // Създаване на временен URL от избрания файл и показване на изображението в реално време
            setCoverPhotoUrlCurrent(URL.createObjectURL(selectedFile));
        }
    };
    async function onSubmit(values) {
        values = {
            ...values,
            [ProfileFormKeys.Gender]: ['0', '1'].includes(
                values[ProfileFormKeys.Gender]
            )
                ? Number(values[ProfileFormKeys.Gender])
                : '',
            [ProfileFormKeys.Relationship]: ['0', '1', '2'].includes(
                values[ProfileFormKeys.Relationship]
            )
                ? Number(values[ProfileFormKeys.Relationship])
                : '',
        };

        const fileInput = document.getElementById(
            ProfileFormKeys.ProfilePicture
        );

        const selectedFile = fileInput.files[0];
        const fileInputCover = document.getElementById(
            ProfileFormKeys.CoverPhoto
        );

        const selectedFileCover = fileInputCover.files[0];

        const formData = new FormData();

        // Добавете полетата от values към formData
        formData.append('FirstName', values.FirstName);
        formData.append('LastName', values.LastName);
        formData.append('PhoneNumber', values.PhoneNumber);
        formData.append('ProfilePicture', selectedFile);
        formData.append('CoverPhoto', selectedFileCover);
        formData.append('Country', values.Country);
        formData.append('State', values.State);
        formData.append('Gender', values.Gender);
        formData.append('School', values.School);
        formData.append('Birthday', values.Birthday);
        formData.append('Relationship', values.Relationship);

        try {
            await userService.update(formData);
        } catch (error) {
            console.log('Error:', error);
        }

        navigate(PATH.userProfile(userId));
    }
    return (
        <div className="user-profile">
            <article className="post-item">
                <form onSubmit={handleSubmit}>
                    <img
                        className="user-cover"
                        src={coverPhotoUrlCurrent}
                        alt="User cover photo"
                    />
                    <div className="user-info-wrapper">
                        <img
                            className="user-img"
                            src={profilePictureUrlCurrent}
                            alt="User profile pic"
                        />

                        <div className="username-profile">
                            <fieldset>
                                <legend>Change the Picture</legend>
                                <label htmlFor={ProfileFormKeys.ProfilePicture}>
                                    ProfilePicture{' '}
                                </label>

                                <input
                                    type="file"
                                    id={ProfileFormKeys.ProfilePicture}
                                    name={ProfileFormKeys.ProfilePicture}
                                    placeholder="Upload a photo..."
                                    onChange={handleChangeProfilePicture}
                                    onBlur={handleBlur}
                                />
                                <div>
                                    <label htmlFor={ProfileFormKeys.CoverPhoto}>
                                        CoverPhoto{' '}
                                    </label>

                                    <input
                                        type="file"
                                        id={ProfileFormKeys.CoverPhoto}
                                        name={ProfileFormKeys.CoverPhoto}
                                        placeholder="Upload a photo..."
                                        onChange={handleChangeCoverPhoto}
                                        onBlur={handleBlur}
                                    />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Contact information</legend>
                        <label htmlFor={ProfileFormKeys.FirstName}>
                            First Name{' '}
                        </label>
                        <input
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
                                <p className="error-message">
                                    {errors[ProfileFormKeys.FirstName]}
                                </p>
                            )}

                        <div>
                            <label htmlFor={ProfileFormKeys.LastName}>
                                Last Name{' '}
                            </label>
                            <input
                                type="text"
                                name={ProfileFormKeys.LastName}
                                id={ProfileFormKeys.LastName}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[ProfileFormKeys.LastName]}
                            />

                            {errors[ProfileFormKeys.LastName] &&
                                touched[ProfileFormKeys.LastName] && (
                                    <p className="error-message">
                                        {errors[ProfileFormKeys.LastName]}
                                    </p>
                                )}
                        </div>

                        <section>
                            <label htmlFor={ProfileFormKeys.PhoneNumber}>
                                Phone Number{' '}
                            </label>
                            <input
                                type="text"
                                name={ProfileFormKeys.PhoneNumber}
                                id={ProfileFormKeys.PhoneNumber}
                                placeholder="Phone number"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[ProfileFormKeys.PhoneNumber]}
                            />

                            {errors[ProfileFormKeys.PhoneNumber] &&
                                touched[ProfileFormKeys.PhoneNumber] && (
                                    <p className="error-message">
                                        {errors[ProfileFormKeys.PhoneNumber]}
                                    </p>
                                )}
                        </section>
                    </fieldset>
                    <fieldset>
                        <legend>Personal data</legend>
                        <section>
                            <label htmlFor={ProfileFormKeys.Country}>
                                Country{' '}
                            </label>
                            <input
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
                                    <p className="error-message">
                                        {errors[ProfileFormKeys.Country]}
                                    </p>
                                )}
                        </section>

                        <section>
                            <label htmlFor={ProfileFormKeys.State}>
                                State{' '}
                            </label>
                            <input
                                type="text"
                                name={ProfileFormKeys.State}
                                id={ProfileFormKeys.State}
                                placeholder="State"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[ProfileFormKeys.State]}
                            />

                            {errors[ProfileFormKeys.State] &&
                                touched[ProfileFormKeys.State] && (
                                    <p className="error-message">
                                        {errors[ProfileFormKeys.State]}
                                    </p>
                                )}
                        </section>

                        <section>
                            <label htmlFor={ProfileFormKeys.Gender}>
                                Gender{' '}
                            </label>
                            <label>
                                <input
                                    type="radio"
                                    name={ProfileFormKeys.Gender}
                                    id={ProfileFormKeys.Gender + '0'}
                                    value="0"
                                    checked={
                                        values[ProfileFormKeys.Gender] === '0'
                                    }
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />{' '}
                                Man{' '}
                            </label>
                            <label>
                                <input
                                    type="radio"
                                    name={ProfileFormKeys.Gender}
                                    id={ProfileFormKeys.Gender + '1'}
                                    value="1"
                                    checked={
                                        values[ProfileFormKeys.Gender] === '1'
                                    }
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />{' '}
                                Woman{' '}
                            </label>
                            <label>
                                <input
                                    type="radio"
                                    name={ProfileFormKeys.Gender}
                                    id={ProfileFormKeys.Gender + '2'}
                                    value="---"
                                    checked={
                                        values[ProfileFormKeys.Gender] === '---'
                                    }
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />{' '}
                                Prefer not to say
                            </label>
                        </section>

                        <section>
                            <label htmlFor={ProfileFormKeys.School}>
                                School{' '}
                            </label>
                            <input
                                type="text"
                                name={ProfileFormKeys.School}
                                id={ProfileFormKeys.School}
                                placeholder="School"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[ProfileFormKeys.School]}
                            />

                            {errors[ProfileFormKeys.School] &&
                                touched[ProfileFormKeys.School] && (
                                    <p className="error-message">
                                        {errors[ProfileFormKeys.School]}
                                    </p>
                                )}
                        </section>

                        <section>
                            <label htmlFor={ProfileFormKeys.Birthday}>
                                Birthday (mm/dd/yyyy){' '}
                            </label>
                            <input
                                type="date"
                                name={ProfileFormKeys.Birthday}
                                id={ProfileFormKeys.Birthday}
                                placeholder="Birthday"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values[ProfileFormKeys.Birthday]}
                            />

                            {errors[ProfileFormKeys.Birthday] &&
                                touched[ProfileFormKeys.Birthday] && (
                                    <p className="error-message">
                                        {errors[ProfileFormKeys.Birthday]}
                                    </p>
                                )}
                        </section>

                        <section>
                            <label htmlFor={ProfileFormKeys.Relationship}>
                                Relationship{' '}
                            </label>

                            <label>
                                <input
                                    type="radio"
                                    name={ProfileFormKeys.Relationship}
                                    id={ProfileFormKeys.Relationship + '0'}
                                    value="0"
                                    checked={
                                        values[ProfileFormKeys.Relationship] ===
                                        '0'
                                    }
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />{' '}
                                Single{' '}
                            </label>

                            <label>
                                <input
                                    type="radio"
                                    name={ProfileFormKeys.Relationship}
                                    id={ProfileFormKeys.Relationship + '1'}
                                    value="1"
                                    checked={
                                        values[ProfileFormKeys.Relationship] ===
                                        '1'
                                    }
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />{' '}
                                InARelationship{' '}
                            </label>

                            <label>
                                <input
                                    type="radio"
                                    name={ProfileFormKeys.Relationship}
                                    id={ProfileFormKeys.Relationship + '2'}
                                    value="2"
                                    checked={
                                        values[ProfileFormKeys.Relationship] ===
                                        '2'
                                    }
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />{' '}
                                Married{' '}
                            </label>
                            <label>
                                <input
                                    type="radio"
                                    name={ProfileFormKeys.Relationship}
                                    id={ProfileFormKeys.Relationship + '3'}
                                    value="---"
                                    checked={
                                        values[ProfileFormKeys.Relationship] ===
                                        '---'
                                    }
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />{' '}
                                Prefer not to say
                            </label>
                        </section>
                    </fieldset>
                    <div className="parent-button">
                        <button
                            className="profile-button"
                            type="submit"
                            disabled={isSubmitting}
                        >
                            Submit
                        </button>
                    </div>
                </form>
            </article>
        </div>
    );
}
