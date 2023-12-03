import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';

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

    useEffect(() => {
        agent.apiAnimal.getAllAnimals()
            .then(res => {
                setPets(res.data);
            });
    }, []);

    return (
        <section className={themeStore.isLightTheme ? 'dashboard__wrapper' : 'dashboard__wrapper dashboard__wrapper__dark'}>

            <article className='dashboard__greet'>
                <CLogo/>
                <h1 className={themeStore.isLightTheme ? 'greet__title' :'greet__title dashboard__greet__dark'}>welcome, {userStore.user?.Name} !</h1>
                <CChangeThemeButton />
            </article>

            <article className={themeStore.isLightTheme ?'dashboard__article ' : 'dashboard__article dashboard__article__dark '}>
                <h3>my pets</h3>
                <section className='dashboard__pets'>
                    {pets && pets.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} key={x.id} />)}
                    <CAddPetCard />
                </section>
            </article>

            <article className={themeStore.isLightTheme ? 'dashboard__article': 'dashboard__article dashboard__article__dark'}>
                <h3>my profile</h3>
                <UserProfile />
            </article>

        </section>
    );
});
