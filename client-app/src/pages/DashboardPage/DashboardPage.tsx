import React from 'react';
import { observer } from 'mobx-react';
import { CPetCard } from '../../components/common/CPetCard/CPetCard';
import { CAddPetCard } from '../../components/common/CAddPetCard/CAddPetCard';
import { UserProfile } from '../../components/UserProfile/UserProfile';
import { CLogo } from '../../components/common/CLogo/CLogo';
import { CChangeThemeButton } from '../../components/common/CChangeThemeButton/CChangeThemeButton';
import themeStore from '../../stores/themeStore';
import './DashboardPage.scss';
import userStore from '../../stores/userStore';

const pets = [
    {
        name: "Eminem",
        photo: "https://cdn.theatlantic.com/thumbor/vDZCdxF7pRXmZIc5vpB4pFrWHKs=/559x0:2259x1700/1080x1080/media/img/mt/2017/06/shutterstock_319985324/original.jpg"
    },
    {
        name: "Vanilla Ice",
        photo: "https://highlandcanine.com/wp-content/uploads/2021/01/vizsla-running.jpg"
    },
    {
        name: "2pac",
        photo: "https://i.guim.co.uk/img/media/26392d05302e02f7bf4eb143bb84c8097d09144b/446_167_3683_2210/master/3683.jpg?width=1200&height=900&quality=85&auto=format&fit=crop&s=11e949fc5d06576bc8b80ec192896753"
    },
    {
        name: "snoop dogg",
        photo: "https://a-z-animals.com/media/2021/12/Prettiest-_-Cutest-Dogs-header.jpg"
    },
    {
        name: "BIG",
        photo: "https://i.pinimg.com/originals/0d/08/60/0d0860d917320784369a58a1f01187d3.jpg"
    },
]


interface DashboardPageProps { }

export const DashboardPage: React.FC<DashboardPageProps> = observer(() => {
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
                    {pets.map(x => <CPetCard name={x.name} photo={x.photo} key={x.name} />)}
                    <CAddPetCard />
                </section>
            </article>

            <article className={themeStore.isLightTheme ? 'dashboard__article': 'dashboard__article dashboard__article__dark'}>
                <h3>my profile</h3>
                <UserProfile />
            </article>

        </section>
    )
})
