/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.  

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided “AS IS” WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/

using UnityEngine;
using UnityEngine.Assertions;

namespace OculusSampleFramework
{
	public class CellPartController : MonoBehaviour
	{
		[SerializeField] private GameObject _startStopButton = null;
		[SerializeField] float _maxSpeed = 10f;
		[SerializeField] private SelectionCylinder _selectionCylinder = null;
		[SerializeField] private GameObject _topLight = null;


		//This component will be looked for on awake
		private WindmillBladesController _bladesRotation;
		private InteractableTool _toolInteractingWithMe = null;

		private void Awake()
		{
			//Assert that the button gameobject and the selectioncylinder reference are not null
			Assert.IsNotNull(_startStopButton);
			Assert.IsNotNull(_selectionCylinder);

			//Gets the blades controller script in the children object
			_bladesRotation = GetComponentInChildren<WindmillBladesController>();

			//Default blades movement will be true with the given float speed
			_bladesRotation.SetMoveState(true, _maxSpeed);
		}


		//When this controller is enabled a listener for changed state is added ?
		private void OnEnable()
		{
			//THIS MEANS "ON PINCH"
			_startStopButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(StartStopStateChanged);
		}

		//When this controller is disabled and if the gameobject from _startstopbutton isnt null a listener for changed state is removed ?
		private void OnDisable()
		{
			if (_startStopButton != null)
			{
				_startStopButton.GetComponent<Interactable>().InteractableStateChanged.RemoveListener(StartStopStateChanged);
			}
		}

		//When there is a change in state, do something
		private void StartStopStateChanged(InteractableStateArgs obj)
		{
			//A bool is declared to... give it the value of the state in which the interactablestate is in? in this case, actionstate is equal to 3
			bool inActionState = obj.NewInteractableState == InteractableState.ActionState;
			if (inActionState)
			{

				//When blade is rotating and a state change is fired, set speed to 0 and setmovestate to false
				if (_bladesRotation.IsMoving)
				{
					_bladesRotation.SetMoveState(false, 0.0f);
				}
				else
				{
					_bladesRotation.SetMoveState(true, _maxSpeed);
				}
			}

			//What does this do? Its related to the InteractableStateArgs 
			_toolInteractingWithMe = obj.NewInteractableState > InteractableState.Default ?
			  obj.Tool : null;
		}


		//Happening every frame
		//Verifying if there is a tool interacting with the object, if so, toggle the selectionstate to highlighted then to selected, if not, toggle selectionstate to off
		private void Update()
		{
			if (_toolInteractingWithMe == null)
			{
				_selectionCylinder.CurrSelectionState = SelectionCylinder.SelectionState.Off;
				//My test with the light
				_topLight.SetActive(true);

			}
			else
			{

				//Ask what these lines do... they change states, but what's up with ? and :
				_selectionCylinder.CurrSelectionState = (
				  _toolInteractingWithMe.ToolInputState == ToolInputState.PrimaryInputDown ||
				  _toolInteractingWithMe.ToolInputState == ToolInputState.PrimaryInputDownStay)
				  ? SelectionCylinder.SelectionState.Highlighted
				  : SelectionCylinder.SelectionState.Selected;
				//My test with the light
				_topLight.SetActive(false);

			}
		}
	}
}
