import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';
import { FcHome, FcShop } from 'react-icons/fc';
import { MdPets } from 'react-icons/md';

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

interface UserAnimals {
    id: string,
    name: string,
    mainPhoto: string
}

export const DashboardPage: React.FC<DashboardPageProps> = observer(() => {
    const [pets, setPets] = useState<UserAnimals[]>([]);
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

    const onAdoptionplaceClick = () => {
        setPlace('adoption');
    };

    const onMarketplaceClick = () => {
        setPlace('market');
    };

    return (
        <section className={themeStore.isLightTheme ? 'dashboard__wrapper' : 'dashboard__wrapper dashboard__wrapper__dark'}>

            <article className='dashboard__greet'>
                <CLogo />
                <h1 className={themeStore.isLightTheme ? 'greet__title' : 'greet__title dashboard__greet__dark'}>welcome, {userStore.user?.Name} !</h1>
                <CChangeThemeButton />
            </article>

            <nav className='dashboard__navbar'>
                <div className='dashboard__navbar__icons'>
                    <FcHome onClick={onHomeClick} />
                    <MdPets onClick={onAdoptionplaceClick} />
                    <FcShop onClick={onMarketplaceClick} />
                </div>
            </nav>

            {place == 'home' && (
                <article className={themeStore.isLightTheme ? 'dashboard__article ' : 'dashboard__article dashboard__article__dark '}>
                    <h3>my pets</h3>
                    <section className='dashboard__pets'>
                        {pets && pets.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} key={x.id} />)}
                        <CAddPetCard />
                    </section>
                </article>
            )}

            {place == 'adoption' && (
                <article className={themeStore.isLightTheme ? 'dashboard__article ' : 'dashboard__article dashboard__article__dark '}>
                    <h3>Adoption place</h3>
                    <section className='dashboard__pets'>

                        <CAddPetCard />
                    </section>
                </article>
            )}

            {place == 'market' && (
                <article className={themeStore.isLightTheme ? 'dashboard__article ' : 'dashboard__article dashboard__article__dark '}>
                    <h3>Marketplace</h3>
                    <section className='dashboard__pets'>

                        <CAddPetCard />
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
