import React from 'react';
import { IUser } from '../../interfaces/Interfaces';
import { CCardEditButton } from '../common/CCardEditButton/CCardEditButton';
import themeStore from '../../stores/themeStore';
import './UserProfile.scss';

interface UserProfileProps { }

const user: IUser = {
    id: '123',
    name: 'john sullivan',
    email: 'john.sullivan@grb.gr',
    age: 25,
    education: 'economic',
    photo: 'https://i.pinimg.com/1200x/8b/16/7a/8b167af653c2399dd93b952a48740620.jpg',
    jobTitle: 'sales manager',
    gender: 'male',
    address: '1313 E Main St, Portage MI',
    city: 'Portage'
}
export const UserProfile: React.FC<UserProfileProps> = () => {
    return (
        <section className='dashboard__user__profile'>
            <article className='dashboard__user__profile__image__wrapper'>
                <img src={user.photo} alt="" />
            </article>
            <h2 className={themeStore.isLightTheme ? '' : 'user__name__dark'}>{user.name}</h2>
            <ul className='dashboard__user__profile__content'>
                <li>
                    <h4>email:</h4>
                    <p>{user.email}</p>
                </li>
                <li>
                    <h4>gender:</h4>
                    <p>{user.gender}</p>
                </li>
                <li>
                    <h4>address:</h4>
                    <p>{user.address}</p>
                </li>
                <li>
                    <h4>education:</h4>
                    <p>{user.education}</p>
                </li>
                <li>
                    <h4>city:</h4>
                    <p>{user.city}</p>
                </li>
                <li>
                    <h4>job:</h4>
                    <p>{user.jobTitle}</p>
                </li>
                <li>
                    <h4>age:</h4>
                    <p>{user.age}</p>
                </li>
            </ul>

            <div className='dashboard__user__profile__buttons__wrapper'>
                <CCardEditButton />
                <CCardEditButton />
                <CCardEditButton />
            </div>
        </section>
    )
}
