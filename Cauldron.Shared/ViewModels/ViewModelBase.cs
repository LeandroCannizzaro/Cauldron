﻿using Cauldron.Core;
using Cauldron.Validation;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cauldron.ViewModels
{
    /// <summary>
    /// Represents the Base class of a ViewModel
    /// </summary>
    public abstract class ViewModelBase : IViewModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ViewModelBase"/>
        /// </summary>
        [Inject]
        public ViewModelBase()
        {
            this.Id = Guid.NewGuid();
            this.Dispatcher = new CauldronDispatcher();
            this.Navigator = Factory.Create<INavigator>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ViewModelBase"/>
        /// </summary>
        /// <param name="id">A unique identifier of the viewmodel</param>
        public ViewModelBase(Guid id)
        {
            this.Id = id;
            this.Dispatcher = new CauldronDispatcher();
            this.Navigator = Factory.Create<INavigator>();
        }

        /// <summary>
        /// Occures if a behaviour should be invoked
        /// </summary>
        public event EventHandler<BehaviourInvocationArgs> BehaviourInvoke;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the <see cref="Dispatcher"/> this <see cref="CauldronDispatcher "/> is associated with.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced), JsonIgnore]
        public CauldronDispatcher Dispatcher { get; private set; }

        /// <summary>
        /// Gets the unique Id of the view model
        /// </summary>
        [SuppressIsChanged, JsonIgnore]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the window navigator
        /// </summary>
        protected INavigator Navigator { get; private set; }

        /// <summary>
        /// Invokes the <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="propertyName">The name of the property where the value change has occured</param>
        public async void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (propertyName.EndsWith("Command"))
                return;

            if (this.BeforeRaiseNotifyPropertyChanged(propertyName))
                return;

            if (this.PropertyChanged != null)
                await this.Dispatcher.RunAsync(() => this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));

            this.AfterRaiseNotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// Occures after the event <see cref="PropertyChanged"/> has been invoked
        /// </summary>
        /// <param name="propertyName">The name of the property where the value change has occured</param>
        protected virtual void AfterRaiseNotifyPropertyChanged(string propertyName)
        {
        }

        /// <summary>
        /// Occured before the <see cref="PropertyChanged"/> event is invoked.
        /// </summary>
        /// <param name="propertyName">The name of the property where the value change has occured</param>
        /// <returns>Returns true if <see cref="RaisePropertyChanged(string)"/> should be cancelled. Otherwise false</returns>
        protected virtual bool BeforeRaiseNotifyPropertyChanged(string propertyName)
        {
            return false;
        }

        /// <summary>
        /// Invokes the <see cref="BehaviourInvoke"/> event
        /// </summary>
        /// <param name="behaviourName">The name of the behaviour to invoke</param>
        protected async void RaiseNotifyBehaviourInvoke(string behaviourName)
        {
            if (this.BehaviourInvoke != null)
                await this.Dispatcher.RunAsync(() => this.BehaviourInvoke(this, new BehaviourInvocationArgs(behaviourName)));
        }
    }
}