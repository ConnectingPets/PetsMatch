import React, {useState} from 'react';

import './LoginRegisterPage.scss';

interface LoginRegisterPageProps { };

export const LoginRegisterPage: React.FC<LoginRegisterPageProps> = () => {

    const [isRegistered, setIsRegistered] = useState<boolean>(true)

    return (
        <div className='login__register__wrapper'>
            <section className='login__register__options'>
                <div>
                    <h3>login</h3>
                </div>
                <div>
                    <h3>register</h3>
                </div>
            </section>
            <section className='login__register__forms__wrapper'>

            </section>
        </div>
    )
}
