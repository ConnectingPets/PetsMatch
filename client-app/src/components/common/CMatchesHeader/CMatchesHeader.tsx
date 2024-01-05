import React, {useEffect, useState} from 'react';
import { Link, useParams } from 'react-router-dom';
import { FaShieldDog } from 'react-icons/fa6';
import { observer } from 'mobx-react';
import themeStore from '../../../stores/themeStore';
import './CMatchesHeader.scss';
import agent from '../../../api/axiosAgent';

interface CMatchesHeaderProps { }

interface IMatchesHeaderAnimal {
    name: string,
    photo: string
}

export const CMatchesHeader: React.FC<CMatchesHeaderProps> = observer(() => {
    const [pet, setPet] = useState<IMatchesHeaderAnimal>();
    const {id} = useParams();

    useEffect(() => {
        if (id) {
            agent.apiAnimal.getAnimalById(id!)
                .then(res => {
                    const {name, photos} = res.data;

                    setPet({
                        name,
                        photo: photos.find((p: {
                            isMain: boolean
                        }) => p.isMain).url
                    })
                })
        }
    }, [id])

    return (
        <section className={themeStore.isLightTheme ? 'matches__header' : 'matches__header  matches__header__dark'}>
            <article className='matches__header__user'>
                <div className='matches__header__user__image'>
                    <img src={pet?.photo} alt="" />
                </div>
                <h5>{pet?.name}</h5>
            </article>
            <Link to={'/about-faq'}>
                <FaShieldDog />
            </Link>
        </section>
    )
})
