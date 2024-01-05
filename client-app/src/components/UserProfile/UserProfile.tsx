import React from 'react';
import { Link, useNavigate } from 'react-router-dom';

import themeStore from '../../stores/themeStore';
import userStore from '../../stores/userStore';
import agent from '../../api/axiosAgent';

import './UserProfile.scss';
import { toast } from 'react-toastify';

interface UserProfileProps { }

export const UserProfile: React.FC<UserProfileProps> = () => {
    const navigate = useNavigate();

    const onLogoutClick = async () => {
        try {
            const result = await agent.apiUser.logout({});

            if (result.isSuccess) {
                userStore.clearUser();
                navigate('/');
            } else {
                toast.error(result.errorMessage);
            }
        } catch(err) {
            console.error(err);
        }
    };

    return (
        <section className='dashboard__user__profile'>
            <article className='dashboard__user__profile__image__wrapper'>
                {userStore.user?.PhotoUrl
                    ? <img src={userStore.user?.PhotoUrl} alt="user profile image" />
                    : <img src="/images/user-profile-pic.jpg" alt="default user profile picture" />
                }
            </article>
            <h2 className={themeStore.isLightTheme ? '' : 'user__name__dark'}>{userStore.user?.Name}</h2>
            <ul className='dashboard__user__profile__content'>
                <li>
                    <h4>email:</h4>
                    <p>{userStore.user?.Email}</p>
                </li>
                <li>
                    <h4>age:</h4>
                    <p>{userStore.user?.Age}</p>
                </li>
                <li>
                    <h4>gender:</h4>
                    <p>{userStore.user?.Gender}</p>
                </li>
                <li>
                    <h4>address:</h4>
                    <p>{userStore.user?.Address}</p>
                </li>
                <li>
                    <h4>city:</h4>
                    <p>{userStore.user?.City}</p>
                </li>
                <li>
                    <h4>education:</h4>
                    <p>{userStore.user?.Education}</p>
                </li>
                <li>
                    <h4>job:</h4>
                    <p>{userStore.user?.JobTitle}</p>
                </li>
            </ul>

            <div className='dashboard__user__profile__buttons__wrapper'>
                <Link to="/user/edit-profile" className={themeStore.isLightTheme ? '' : 'dark'}>Edit Profile</Link>
                <button className={themeStore.isLightTheme ? '' : 'dark'}>Change Password</button>
                <button onClick={onLogoutClick} className={themeStore.isLightTheme ? '' : 'dark'}>Logout</button>
            </div>
        </section>
    );
};
