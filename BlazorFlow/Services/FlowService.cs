﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorFlow.Helpers;

namespace BlazorFlow.Data
{
    public class FlowService
    {
        public Task<Flow> GetFlow(int flowVersion)
        {
            return Task.FromResult(BuildFlow(flowVersion));
        }

        Flow BuildFlow(int flowVersion)
        {
            var flowQuestions = BuildFlowQuestions();
            var flowAnswers = BuildFlowAnswers();

            Flow flow = new Flow(1, flowVersion);

            var node1 = new FlowNode(1, 1, flowQuestions.First(x => x.FlowQuestionCode == "START"), FlowNodeType.none);
            var node2 = new FlowNode(2, 1, flowQuestions.First(x => x.FlowQuestionCode == "RUREADY"), FlowNodeType.singleChoice, new FlowAnswer[] { flowAnswers[0], flowAnswers[1] });
            var node3 = new FlowNode(3, 1, flowQuestions.First(x => x.FlowQuestionCode == "NOTREADY"), FlowNodeType.none);
            var node4 = new FlowNode(4, 1, flowQuestions.First(x => x.FlowQuestionCode == "2RUMBLE"), FlowNodeType.numberCompare);
            var node5 = new FlowNode(5, 1, flowQuestions.First(x => x.FlowQuestionCode == "NOTENOUGH"), FlowNodeType.none);
            var node6 = new FlowNode(6, 1, flowQuestions.First(x => x.FlowQuestionCode == "HOW2RUMBLE"), FlowNodeType.multiChoice, new FlowAnswer[] { flowAnswers[2], flowAnswers[3], flowAnswers[4], flowAnswers[5] });
            var node7 = new FlowNode(7, 1, flowQuestions.First(x => x.FlowQuestionCode == "CANTRUMBLE"), FlowNodeType.none);
            var node8 = new FlowNode(8, 1, flowQuestions.First(x => x.FlowQuestionCode == "END"), FlowNodeType.none);

            var cond1 = new FlowCondition("NO");
            var cond2 = new FlowCondition("YES");
            var cond3 = new FlowCondition(OperationHelper.LessThan<decimal>(), 9000);
            var cond4 = new FlowCondition(OperationHelper.GreaterThanOrEqualTo<decimal>(), 9000);
            var cond5 = new FlowCondition(new HashSet<string> { "GOOD", "GREAT", "AMAZING" });

            var link1 = new FlowLink(1, 1, node1, node2);
            var link2 = new FlowLink(2, 1, node2, node3, cond1);
            var link3 = new FlowLink(3, 1, node2, node4, cond2);
            var link4 = new FlowLink(4, 1, node4, node5, cond3);
            var link5 = new FlowLink(5, 1, node4, node6, cond4);
            var link6 = new FlowLink(6, 1, node6, node7);
            var link7 = new FlowLink(7, 1, node6, node8, cond5);

            flow.AddVertex(node1);
            flow.AddVertex(node2);
            flow.AddVertex(node3);
            flow.AddVertex(node4);
            flow.AddVertex(node5);
            flow.AddVertex(node6);
            flow.AddVertex(node7);
            flow.AddVertex(node8);

            flow.AddEdge(link1);
            flow.AddEdge(link2);
            flow.AddEdge(link3);
            flow.AddEdge(link4);
            flow.AddEdge(link5);
            flow.AddEdge(link6);
            flow.AddEdge(link7);

            return flow;
        }

        FlowQuestion[] BuildFlowQuestions()
        {
            return new FlowQuestion[]
            {
                new FlowQuestion(1, "START", "Welcome! You're starting a new application.", ""),
                new FlowQuestion(2, "RUREADY", "Are you ready to rumble?", ""),
                new FlowQuestion(3, "NOTREADY", "You must be able ready to rumble to continue.", ""),
                new FlowQuestion(4, "2RUMBLE", "How many times have you rumbled?", ""),
                new FlowQuestion(5, "NOTENOUGH", "You must have rumbled at least 9000 times to continue.", ""),
                new FlowQuestion(6, "HOW2RUMBLE", "What are your favorite ways to rumble?", ""),
                new FlowQuestion(7, "CANTRUMBLE", "You're not rumbling properly...", ""),
                new FlowQuestion(8, "END", "Done! Now go away.", "")
            };
        }

        FlowAnswer[] BuildFlowAnswers()
        {
            return new FlowAnswer[]
            {
                new FlowAnswer(1, "YES", "YES", "Yes", "Oui"),
                new FlowAnswer(2, "NO", "NO", "No", "Non"),

                new FlowAnswer(3, "GOOD", "GOOD", "Good", "Bien"),
                new FlowAnswer(4, "GREAT", "GREAT", "Great", "Super"),
                new FlowAnswer(5, "AMAZING", "AMAZING", "Amazing", "Extraordinaire"),
                new FlowAnswer(6, "BAD", "BAD", "Bad", "Mauvais"),
            };
        }
    }
}
