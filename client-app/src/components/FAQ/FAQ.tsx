import React from 'react';
import { useState } from 'react';
import { Link } from 'react-router-dom';

import { FiPlus } from 'react-icons/fi';
import { MdClear } from 'react-icons/md';
import './FAQ.scss';

import { questionsData } from './questionData';

interface FAQProps { }

const Faq: React.FC<FAQProps> = () => {
    const [isOpenQuestionId, setIsOpenQuestionId] = useState<number | null>(null);

    const onOpenCloseQuestion = (id: number) => {
        if (isOpenQuestionId === id) {
            setIsOpenQuestionId(null);
        } else {
            setIsOpenQuestionId(id);
        }
    };

    return (
        <>
            <div className="info__container__faq-banner">
                <div>
                    <h3>Got a question?</h3>
                    <h3>Get your answer</h3>
                    <span>or</span>
                    <Link to='/login-register'>Login / Sign Up</Link>
                </div>
                <div className="info__container__faq-banner__img">
                    <img src="/images/faq-dog.jpg" alt="dog" />
                </div>
            </div>

            <h2>Frequently asked questions</h2>

            <section className="info__container__qa">
                {questionsData && questionsData.map(q => (
                    <div onClick={() => onOpenCloseQuestion(q.id)} key={q.id} className="info__container__qa__question">
                        <div>
                            <p>{q.question}</p>
                            {isOpenQuestionId !== q.id ? <FiPlus /> : <MdClear />}
                        </div>

                        {isOpenQuestionId === q.id && (
                            <p>{q.answer}</p>
                        )}
                    </div>
                ))}
            </section>
        </>
    );
};

export default Faq;