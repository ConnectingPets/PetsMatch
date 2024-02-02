import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';
import { FcShop } from 'react-icons/fc';
import { MdPets } from 'react-icons/md';

import { IUserAnimals, IUserProfile } from '../../interfaces/Interfaces';
import themeStore from '../../stores/themeStore';
import './DashboardPage.scss';
import agent from '../../api/axiosAgent';

import { CPetCard } from '../../components/common/CPetCard/CPetCard';
import { CAddPetCard } from '../../components/common/CAddPetCard/CAddPetCard';
import { UserProfile } from '../../components/UserProfile/UserProfile';
import { CLogo } from '../../components/common/CLogo/CLogo';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';
import { CMarketCardButton } from '../../components/common/CMarketCardButton/CMarketCardButton';
import ChangePasswordModal from '../../components/ChangePasswordModal/ChangePasswordModal';
import Footer from '../../components/Footer/Footer';

interface DashboardPageProps { }

export const DashboardPage: React.FC<DashboardPageProps> = observer(() => {
    const [user, setUser] = useState<IUserProfile | undefined>(undefined);
    const [pets, setPets] = useState<IUserAnimals[]>([]);
    const [petsInMarket, setPetsInMarket] = useState<IUserAnimals[]>([]);
    const [petsForAdoption, setPetsForAdoption] = useState<IUserAnimals[]>([]);
    const [place, setPlace] = useState<string>('home');
    const [isHaveTwoRoles, setIsHaveTwoRoles] = useState<boolean>(false);
    const [isClickChangePassword, setIsClickChangePassword] = useState<boolean>(false);

    useEffect(() => {
        agent.apiUser.getUserProfile()
            .then(res => {
                if (res.data.roles.includes('Matching')) {
                    agent.apiAnimal.getAllAnimals()
                        .then(res => {
                            setPets(res.data);
                        });
                }

                if (res.data.roles.includes('Marketplace') && res.data.roles.includes('Matching')) {
                    setIsHaveTwoRoles(true);
                } else if (res.data.roles.includes('Marketplace')) {
                    setPlace('market');
                    
                    onMarketplaceClick();
                }

                setUser(res.data);
            });
    }, []);

    const onHomeClick = () => {
        setPlace('home');
    };

    const onAdoptionplaceClick = async () => {
        setPlace('adoption');

        try {
            const res = await agent.apiAdoption.getMyAnimalsForAdoption();

            setPetsForAdoption(res.data);
        } catch (err) {
            console.error(err);
        }
    };

    const onMarketplaceClick = async () => {
        setPlace('market');

        try {
            const res = await agent.apiMarketplace.getMyAnimalsForSale();

            setPetsInMarket(res.data);
        } catch (err) {
            console.error(err);
        }
    };

    const onClickChangePassword = () => {
        setIsClickChangePassword(state => !state);
    };

    return (
        <section className={themeStore.isLightTheme ? 'dashboard__wrapper' : 'dashboard__wrapper dashboard__wrapper__dark'}>

            <article className='dashboard__greet'>
                <CLogo />
                <h1 className={themeStore.isLightTheme ? 'greet__title' : 'greet__title dashboard__greet__dark'}>welcome, {user?.name} !</h1>
                <CChangeThemeButton />
            </article>

            <nav className='dashboard__navbar'>
                {isHaveTwoRoles && (
                    <>
                        <MdPets onClick={onHomeClick} />
                        <FcShop onClick={onMarketplaceClick} />
                    </>
                )}
            </nav>

            {place == 'home' && (
                <article className={themeStore.isLightTheme ? 'dashboard__article ' : 'dashboard__article dashboard__article__dark '}>
                    <h3>my pets</h3>
                    <section className='dashboard__pets'>
                        {pets && pets.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='myPets' key={x.id} />)}
                        <CAddPetCard />
                    </section>
                </article>
            )}

            {place != 'home' && (
                <article className={themeStore.isLightTheme ? 'dashboard__article ' : 'dashboard__article dashboard__article__dark '}>
                    <div className="dashboard__article__market-btn">
                        <CMarketCardButton button='Go to Market' />
                    </div>
                    <h3>My Pets</h3>
                    <section className='dashboard__article__options'>
                        <div>
                            <h3 className={place == 'market' ? 'dashboard__article__option' : ''} onClick={onMarketplaceClick}>For Sale</h3>
                        </div>
                        <div>
                            <h3 className={place == 'adoption' ? 'dashboard__article__option' : ''} onClick={onAdoptionplaceClick}>For Adoption</h3>
                        </div>
                    </section>

                    <section className='dashboard__pets'>
                        {place == 'adoption' && petsForAdoption && petsForAdoption.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='myPetsInMarket' key={x.id} />)}

                        {place == 'market' && petsInMarket && petsInMarket.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='myPetsInMarket' key={x.id} />)}
                        <CAddPetCard link='market' />
                    </section>
                </article>
            )}

            <article className={themeStore.isLightTheme ? 'dashboard__article' : 'dashboard__article dashboard__article__dark'}>
                <h3>my profile</h3>
                <UserProfile user={user} onClickChangePassword={onClickChangePassword} />
            </article>

            {isClickChangePassword && <ChangePasswordModal onClickChangePassword={onClickChangePassword} />}

            <Footer />
        </section>
    );
});
