import React from 'react';
import { Link } from 'react-router-dom';
import { IUser } from '../../interfaces/Interfaces';
import themeStore from '../../stores/themeStore';
import './UserProfile.scss';

interface UserProfileProps { }

const user: IUser = {
    Id: '123',
    Name: 'john sullivan',
    Email: 'john.sullivan@grb.gr',
    Age: 25,
    Education: 'economic',
    Photo: 'https://i.pinimg.com/1200x/8b/16/7a/8b167af653c2399dd93b952a48740620.jpg',
    JobTitle: 'sales manager',
    Gender: 'male',
    Address: '1313 E Main St, Portage MI',
    City: 'Portage'
};

export const UserProfile: React.FC<UserProfileProps> = () => {
    return (
        <section className='dashboard__user__profile'>
            <article className='dashboard__user__profile__image__wrapper'>
                <img src={user.Photo} alt="" />
            </article>
            <h2 className={themeStore.isLightTheme ? '' : 'user__name__dark'}>{user.Name}</h2>
            <ul className='dashboard__user__profile__content'>
                <li>
                    <h4>email:</h4>
                    <p>{user.Email}</p>
                </li>
                <li>
                    <h4>address:</h4>
                    <p>{user.Address}</p>
                </li>
                <li>
                    <h4>gender:</h4>
                    <p>{user.Gender}</p>
                </li>
                <li>
                    <h4>city:</h4>
                    <p>{user.City}</p>
                </li>
                <li>
                    <h4>education:</h4>
                    <p>{user.Education}</p>
                </li>
                <li>
                    <h4>age:</h4>
                    <p>{user.Age}</p>
                </li>
                <li>
                    <h4>job:</h4>
                    <p>{user.JobTitle}</p>
                </li>
            </ul>

            <div className='dashboard__user__profile__buttons__wrapper'>
                <Link to="/user/123abc/edit-profile" className={themeStore.isLightTheme ? '' : 'dark'}>Edit Profile</Link>
                <button className={themeStore.isLightTheme ? '' : 'dark'}>Change Password</button>
                <button className={themeStore.isLightTheme ? '' : 'dark'}>Logout</button>
            </div>
        </section>
    );
};
