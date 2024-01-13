import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';
import { FcShop } from 'react-icons/fc';
import { MdPets } from 'react-icons/md';

import { IUserAnimals } from '../../interfaces/Interfaces';
import themeStore from '../../stores/themeStore';
import './DashboardPage.scss';
import userStore from '../../stores/userStore';
import agent from '../../api/axiosAgent';

import { CPetCard } from '../../components/common/CPetCard/CPetCard';
import { CAddPetCard } from '../../components/common/CAddPetCard/CAddPetCard';
import { UserProfile } from '../../components/UserProfile/UserProfile';
import { CLogo } from '../../components/common/CLogo/CLogo';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';

interface DashboardPageProps { }

export const DashboardPage: React.FC<DashboardPageProps> = observer(() => {
    const [pets, setPets] = useState<IUserAnimals[]>([]);
    const [petsInMarket, setPetsInMarket] = useState<IUserAnimals[]>([]);
    const [petsForAdoption, setPetsForAdoption] = useState<IUserAnimals[]>([]);
    const [place, setPlace] = useState<string>('home');

    useEffect(() => {
        agent.apiAnimal.getAllAnimals()
            .then(res => {
                setPets(res.data);
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

    return (
        <section className={themeStore.isLightTheme ? 'dashboard__wrapper' : 'dashboard__wrapper dashboard__wrapper__dark'}>

            <article className='dashboard__greet'>
                <CLogo />
                <h1 className={themeStore.isLightTheme ? 'greet__title' : 'greet__title dashboard__greet__dark'}>welcome, {userStore.user?.Name} !</h1>
                <CChangeThemeButton />
            </article>

            <nav className='dashboard__navbar'>
                <MdPets onClick={onHomeClick} />
                <FcShop onClick={onMarketplaceClick} />
            </nav>

            {place == 'home' && (
                <article className={themeStore.isLightTheme ? 'dashboard__article ' : 'dashboard__article dashboard__article__dark '}>
                    <h3>my pets</h3>
                    <section className='dashboard__pets'>
                        {pets && pets.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='edit' key={x.id} />)}
                        <CAddPetCard />
                    </section>
                </article>
            )}

            {place != 'home' && (
                <article className={themeStore.isLightTheme ? 'dashboard__article ' : 'dashboard__article dashboard__article__dark '}>
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
                        {place == 'adoption' && petsForAdoption && petsForAdoption.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='market' key={x.id} />)}

                        {place == 'market' && petsInMarket && petsInMarket.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='market' key={x.id} />)}
                        <CAddPetCard link='market' />
                    </section>
                </article>
            )}

            <article className={themeStore.isLightTheme ? 'dashboard__article' : 'dashboard__article dashboard__article__dark'}>
                <h3>my profile</h3>
                <UserProfile />
            </article>

        </section>
    );
});
